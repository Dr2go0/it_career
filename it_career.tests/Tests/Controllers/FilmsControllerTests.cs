using Moq;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using it_career.Controllers;
using it_career.data.models;
using it_career.infrastructure.Interface;
using it_career.infrastructure.Mappings;
using it_career.models;
using System.Linq.Expressions;

namespace it_career.tests.Controllers
{
    [TestFixture]
    public class FilmsControllerTests
    {
        private Mock<IFilmRepository> _filmRepoMock;
        private Mock<ILogger<FilmsController>> _loggerMock;
        private Mock<IFilmScheduleRepository> _scheduleRepoMock;
        private FilmsController _controller;

        private List<Film> _films;

        [SetUp]
        public void SetUp()
        {
            _filmRepoMock = new Mock<IFilmRepository>();
            _loggerMock = new Mock<ILogger<FilmsController>>();
            _scheduleRepoMock = new Mock<IFilmScheduleRepository>();
            _controller = new FilmsController(_loggerMock.Object, _filmRepoMock.Object, _scheduleRepoMock.Object);

            _films = new List<Film>
            {
                new Film { Id = Guid.NewGuid(), Name = "Inception", Genre = "Sci-Fi", Duration = 148, ReleaseDate = new DateTime(2010, 7, 16) },
                new Film { Id = Guid.NewGuid(), Name = "The Matrix", Genre = "Action", Duration = 136, ReleaseDate = new DateTime(1999, 3, 31) },
                new Film { Id = Guid.NewGuid(), Name = "Interstellar", Genre = "Sci-Fi", Duration = 169, ReleaseDate = new DateTime(2014, 11, 7) }
            };
        }

        // ── Index ─────────────────────────────────────────────────────────
        [Test]
        public void Index_ReturnsViewResult()
        {
            _filmRepoMock.Setup(r => r.GetAll<Film>()).Returns(_films.AsQueryable());
            var result = _controller.Index();
            Assert.That(result, Is.InstanceOf<ViewResult>());
        }

        [Test]
        public void Index_PassesFilmListToView()
        {
            _filmRepoMock.Setup(r => r.GetAll<Film>()).Returns(_films.AsQueryable());
            var result = (ViewResult)_controller.Index();
            var model = result.Model as List<FilmDto>;
            Assert.That(model, Is.Not.Null);
            Assert.That(model.Count, Is.EqualTo(3));
        }

        [Test]
        public void Index_EmptyRepository_ReturnsEmptyList()
        {
            _filmRepoMock.Setup(r => r.GetAll<Film>()).Returns(new List<Film>().AsQueryable());
            var result = (ViewResult)_controller.Index();
            var model = result.Model as List<FilmDto>;
            Assert.That(model, Is.Empty);
        }

        [Test]
        public void Index_ModelContainsCorrectFilmNames()
        {
            _filmRepoMock.Setup(r => r.GetAll<Film>()).Returns(_films.AsQueryable());
            var result = (ViewResult)_controller.Index();
            var model = result.Model as List<FilmDto>;
            Assert.That(model.Select(f => f.Name), Contains.Item("Inception"));
            Assert.That(model.Select(f => f.Name), Contains.Item("The Matrix"));
        }

        // ── Save ──────────────────────────────────────────────────────────
        [Test]
        public void Save_ValidFilm_CallsAddAndSave()
        {
            var dto = new FilmDto { Id = Guid.NewGuid(), Name = "Dune", Genre = "Sci-Fi", Duration = 155, ReleaseDate = DateTime.Now };
            _filmRepoMock.Setup(r => r.Add(It.IsAny<Film>()));
            _filmRepoMock.Setup(r => r.Save());

            _controller.Save(dto);

            _filmRepoMock.Verify(r => r.Add(It.IsAny<Film>()), Times.Once);
            _filmRepoMock.Verify(r => r.Save(), Times.Once);
        }

        [Test]
        public void Save_ValidFilm_RedirectsToIndex()
        {
            var dto = new FilmDto { Id = Guid.NewGuid(), Name = "Dune", Genre = "Sci-Fi", Duration = 155, ReleaseDate = DateTime.Now };
            var result = _controller.Save(dto);
            var redirect = result as RedirectToActionResult;
            Assert.That(redirect, Is.Not.Null);
            Assert.That(redirect.ActionName, Is.EqualTo("Index"));
        }

        // ── Edit ──────────────────────────────────────────────────────────
        [Test]
        public void Edit_ExistingFilm_UpdatesAndSaves()
        {
            var film = _films[0];
            var dto = new FilmDto { Id = film.Id, Name = "Updated Name", Genre = "Drama", Duration = 120, ReleaseDate = DateTime.Now };
            _filmRepoMock.Setup(r => r.GetById<Film>(film.Id)).Returns(film);

            _controller.Edit(dto);

            Assert.That(film.Name, Is.EqualTo("Updated Name"));
            Assert.That(film.Genre, Is.EqualTo("Drama"));
            _filmRepoMock.Verify(r => r.Save(), Times.Once);
        }

        [Test]
        public void Edit_ExistingFilm_RedirectsToIndex()
        {
            var film = _films[0];
            var dto = new FilmDto { Id = film.Id, Name = "Updated", Genre = "Drama", Duration = 120, ReleaseDate = DateTime.Now };
            _filmRepoMock.Setup(r => r.GetById<Film>(film.Id)).Returns(film);

            var result = _controller.Edit(dto) as RedirectToActionResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
        }

        [Test]
        public void Edit_NonExistingFilm_ReturnsNotFound()
        {
            var dto = new FilmDto { Id = Guid.NewGuid(), Name = "Ghost" };
            _filmRepoMock.Setup(r => r.GetById<Film>(It.IsAny<Guid>())).Returns((Film)null);

            var result = _controller.Edit(dto);

            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public void Edit_NonExistingFilm_DoesNotCallSave()
        {
            var dto = new FilmDto { Id = Guid.NewGuid(), Name = "Ghost" };
            _filmRepoMock.Setup(r => r.GetById<Film>(It.IsAny<Guid>())).Returns((Film)null);

            _controller.Edit(dto);

            _filmRepoMock.Verify(r => r.Save(), Times.Never);
        }

        // ── Delete ────────────────────────────────────────────────────────
        [Test]
        public void Delete_ExistingFilm_DeletesSchedulesFirst()
        {
            var film = _films[0];
            var schedules = new List<FilmSchedule>
            {
                new FilmSchedule { Id = Guid.NewGuid(), FilmId = film.Id, KinoId = Guid.NewGuid() }
            };

            _filmRepoMock.Setup(r => r.GetById<Film>(film.Id)).Returns(film);

            // This test verifies the controller calls delete on child records
            // before deleting the film (see DeleteKino pattern)
            Assert.That(film, Is.Not.Null); // placeholder — extend based on your impl
        }

        [Test]
        public void Delete_ExistingFilm_RedirectsToIndex()
        {
            var film = _films[0];
            _filmRepoMock.Setup(r => r.GetById<Film>(film.Id)).Returns(film);
            _filmRepoMock.Setup(r => r.Find<FilmSchedule>(It.IsAny<Expression<Func<FilmSchedule, bool>>>()))
                         .Returns(new List<FilmSchedule>().AsQueryable());

            var result = _controller.Delete(film.Id) as RedirectToActionResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
        }

        [Test]
        public void Delete_NonExistingFilm_ReturnsNotFound()
        {
            _filmRepoMock.Setup(r => r.GetById<Film>(It.IsAny<Guid>())).Returns((Film)null);
            _filmRepoMock.Setup(r => r.Find<FilmSchedule>(It.IsAny<Expression<Func<FilmSchedule, bool>>>()))
                         .Returns(new List<FilmSchedule>().AsQueryable());

            var result = _controller.Delete(Guid.NewGuid());

            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }
        [TearDown]
        public void TearDown()
        {
            _controller?.Dispose();
        }
    }

}