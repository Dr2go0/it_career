using Moq;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Linq.Expressions;
using it_career.Controllers;
using it_career.data.models;
using it_career.infrastructure.Interface;
using it_career.models;

namespace it_career.tests.Controllers
{
    [TestFixture]
    public class HomeControllerTests
    {
        private Mock<IKinoRepository> _kinoRepoMock;
        private Mock<IFilmScheduleRepository> _filmScheduleRepoMock;
        private Mock<IFilmRepository> _filmRepoMock;
        private Mock<IBookedFilmRepository> _bookedFilmRepoMock;
        private Mock<IUserRepository> _userRepoMock;
        private Mock<ILogger<HomeController>> _loggerMock;
        private HomeController _controller;

        private List<Kino> _kinos;
        private List<Film> _films;
        private List<FilmSchedule> _schedules;
        private AppUser _user;
        private string _userId = "user-123";

        [SetUp]
        public void SetUp()
        {
            _kinoRepoMock = new Mock<IKinoRepository>();
            _filmScheduleRepoMock = new Mock<IFilmScheduleRepository>();
            _filmRepoMock = new Mock<IFilmRepository>();
            _bookedFilmRepoMock = new Mock<IBookedFilmRepository>();
            _userRepoMock = new Mock<IUserRepository>();
            _loggerMock = new Mock<ILogger<HomeController>>();

            _controller = new HomeController(
                _loggerMock.Object,
                _userRepoMock.Object,
                _kinoRepoMock.Object,
                _filmScheduleRepoMock.Object,
                _filmRepoMock.Object,
                _bookedFilmRepoMock.Object
            );

            // Mock authenticated user
            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, _userId) };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            var principal = new ClaimsPrincipal(identity);
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = principal }
            };

            _user = new AppUser { Id = _userId, Email = "test@test.com", UserName = "test@test.com" };

            _kinos = new List<Kino>
            {
                new Kino { Id = Guid.NewGuid(), Name = "Odeon", Location = "Sofia", Capacity = 200 },
                new Kino { Id = Guid.NewGuid(), Name = "Arena", Location = "Plovdiv", Capacity = 300 }
            };

            _films = new List<Film>
            {
                new Film { Id = Guid.NewGuid(), Name = "Inception", Genre = "Sci-Fi", Duration = 148, ReleaseDate = DateTime.Now }
            };

            _schedules = new List<FilmSchedule>
            {
                new FilmSchedule { Id = Guid.NewGuid(), FilmId = _films[0].Id, KinoId = _kinos[0].Id, ProjectionDate = DateTime.Now.AddDays(2), Film = _films[0] }
            };
        }

        // ── Index ─────────────────────────────────────────────────────────
        [Test]
        public void Index_ReturnsViewResult()
        {
            _kinoRepoMock.Setup(r => r.GetAll<Kino>()).Returns(_kinos.AsQueryable());
            var result = _controller.Index();
            Assert.That(result, Is.InstanceOf<ViewResult>());
        }

        [Test]
        public void Index_ModelContainsAllKinos()
        {
            _kinoRepoMock.Setup(r => r.GetAll<Kino>()).Returns(_kinos.AsQueryable());
            var result = (ViewResult)_controller.Index();
            var model = result.Model as HomeIndexViewModel;
            Assert.That(model.Kinos.Count, Is.EqualTo(2));
        }

        [Test]
        public void Index_ModelContainsCorrectKinoNames()
        {
            _kinoRepoMock.Setup(r => r.GetAll<Kino>()).Returns(_kinos.AsQueryable());
            var result = (ViewResult)_controller.Index();
            var model = result.Model as HomeIndexViewModel;
            Assert.That(model.Kinos.Select(k => k.Name), Contains.Item("Odeon"));
            Assert.That(model.Kinos.Select(k => k.Name), Contains.Item("Arena"));
        }

        [Test]
        public void Index_EmptyKinos_ReturnsEmptyList()
        {
            _kinoRepoMock.Setup(r => r.GetAll<Kino>()).Returns(new List<Kino>().AsQueryable());
            var result = (ViewResult)_controller.Index();
            var model = result.Model as HomeIndexViewModel;
            Assert.That(model.Kinos, Is.Empty);
        }

        // ── KinoSchedule ──────────────────────────────────────────────────
        [Test]
        public void KinoSchedule_ReturnsViewResult()
        {
            var kinoId = _kinos[0].Id;
            _filmScheduleRepoMock
                .Setup(r => r.FindWithIncludes<FilmSchedule>(It.IsAny<Expression<Func<FilmSchedule, bool>>>(), It.IsAny<Expression<Func<FilmSchedule, object>>[]>()))
                .Returns(_schedules.AsQueryable());
            _filmRepoMock.Setup(r => r.GetAll<Film>()).Returns(_films.AsQueryable());
            _kinoRepoMock.Setup(r => r.GetById<Kino>(kinoId)).Returns(_kinos[0]);

            var result = _controller.KinoSchedule(kinoId);

            Assert.That(result, Is.InstanceOf<ViewResult>());
        }

        [Test]
        public void KinoSchedule_ViewModelContainsCorrectKino()
        {
            var kinoId = _kinos[0].Id;
            _filmScheduleRepoMock
                .Setup(r => r.FindWithIncludes<FilmSchedule>(It.IsAny<Expression<Func<FilmSchedule, bool>>>(), It.IsAny<Expression<Func<FilmSchedule, object>>[]>()))
                .Returns(_schedules.AsQueryable());
            _filmRepoMock.Setup(r => r.GetAll<Film>()).Returns(_films.AsQueryable());
            _kinoRepoMock.Setup(r => r.GetById<Kino>(kinoId)).Returns(_kinos[0]);

            var result = (ViewResult)_controller.KinoSchedule(kinoId);
            var model = result.Model as KinoScheduleViewModel;

            Assert.That(model.Kino.Name, Is.EqualTo("Odeon"));
        }

        [Test]
        public void KinoSchedule_ViewModelContainsSchedules()
        {
            var kinoId = _kinos[0].Id;
            _filmScheduleRepoMock
                .Setup(r => r.FindWithIncludes<FilmSchedule>(It.IsAny<Expression<Func<FilmSchedule, bool>>>(), It.IsAny<Expression<Func<FilmSchedule, object>>[]>()))
                .Returns(_schedules.AsQueryable());
            _filmRepoMock.Setup(r => r.GetAll<Film>()).Returns(_films.AsQueryable());
            _kinoRepoMock.Setup(r => r.GetById<Kino>(kinoId)).Returns(_kinos[0]);

            var result = (ViewResult)_controller.KinoSchedule(kinoId);
            var model = result.Model as KinoScheduleViewModel;

            Assert.That(model.FilmSchedules.Count, Is.EqualTo(1));
        }

        // ── SaveKino ──────────────────────────────────────────────────────
        [Test]
        public void SaveKino_ValidKino_CallsAddAndSave()
        {
            var dto = new KinoDto { Id = Guid.NewGuid(), Name = "New Cinema", Location = "Varna", Capacity = 150 };

            _controller.SaveKino(dto);

            _kinoRepoMock.Verify(r => r.Add(It.IsAny<Kino>()), Times.Once);
            _kinoRepoMock.Verify(r => r.Save(), Times.Once);
        }

        [Test]
        public void SaveKino_ValidKino_RedirectsToIndex()
        {
            var dto = new KinoDto { Id = Guid.NewGuid(), Name = "New Cinema", Location = "Varna", Capacity = 150 };
            var result = _controller.SaveKino(dto) as RedirectToActionResult;
            Assert.That(result.ActionName, Is.EqualTo("Index"));
        }

        // ── EditKino ──────────────────────────────────────────────────────
        [Test]
        public void EditKino_ExistingKino_UpdatesFields()
        {
            var kino = _kinos[0];
            var dto = new KinoDto { Id = kino.Id, Name = "Updated", Location = "Burgas", Capacity = 500 };
            _kinoRepoMock.Setup(r => r.GetById<Kino>(kino.Id)).Returns(kino);

            _controller.EditKino(dto);

            Assert.That(kino.Name, Is.EqualTo("Updated"));
            Assert.That(kino.Location, Is.EqualTo("Burgas"));
            Assert.That(kino.Capacity, Is.EqualTo(500));
        }

        [Test]
        public void EditKino_ExistingKino_CallsSave()
        {
            var kino = _kinos[0];
            var dto = new KinoDto { Id = kino.Id, Name = "Updated", Location = "Burgas", Capacity = 500 };
            _kinoRepoMock.Setup(r => r.GetById<Kino>(kino.Id)).Returns(kino);

            _controller.EditKino(dto);

            _kinoRepoMock.Verify(r => r.Save(), Times.Once);
        }

        [Test]
        public void EditKino_NonExistingKino_ReturnsNotFound()
        {
            _kinoRepoMock.Setup(r => r.GetById<Kino>(It.IsAny<Guid>())).Returns((Kino)null);
            var result = _controller.EditKino(new KinoDto { Id = Guid.NewGuid() });
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        // ── DeleteKino ────────────────────────────────────────────────────
        [Test]
        public void DeleteKino_DeletesSchedulesBeforeKino()
        {
            var kino = _kinos[0];
            _kinoRepoMock.Setup(r => r.GetById<Kino>(kino.Id)).Returns(kino);
            _filmScheduleRepoMock
                .Setup(r => r.Find<FilmSchedule>(It.IsAny<Expression<Func<FilmSchedule, bool>>>()))
                .Returns(_schedules.AsQueryable());

            _controller.DeleteKino(kino.Id);

            _filmScheduleRepoMock.Verify(r => r.Delete(It.IsAny<FilmSchedule>()), Times.Once);
            _kinoRepoMock.Verify(r => r.Delete(kino), Times.Once);
        }

        [Test]
        public void DeleteKino_RedirectsToIndex()
        {
            var kino = _kinos[0];
            _kinoRepoMock.Setup(r => r.GetById<Kino>(kino.Id)).Returns(kino);
            _filmScheduleRepoMock
                .Setup(r => r.Find<FilmSchedule>(It.IsAny<Expression<Func<FilmSchedule, bool>>>()))
                .Returns(new List<FilmSchedule>().AsQueryable());

            var result = _controller.DeleteKino(kino.Id) as RedirectToActionResult;

            Assert.That(result.ActionName, Is.EqualTo("Index"));
        }

        [Test]
        public void DeleteKino_NonExistingKino_ReturnsNotFound()
        {
            _kinoRepoMock.Setup(r => r.GetById<Kino>(It.IsAny<Guid>())).Returns((Kino)null);
            _filmScheduleRepoMock
                .Setup(r => r.Find<FilmSchedule>(It.IsAny<Expression<Func<FilmSchedule, bool>>>()))
                .Returns(new List<FilmSchedule>().AsQueryable());

            var result = _controller.DeleteKino(Guid.NewGuid());

            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        // ── SaveSchedule ──────────────────────────────────────────────────
        [Test]
        public void SaveSchedule_ValidSchedule_CallsAddAndSave()
        {
            var dto = new FilmScheduleDto { Id = Guid.NewGuid(), FilmId = _films[0].Id, KinoId = _kinos[0].Id, ProjectionDate = DateTime.Now.AddDays(1) };

            _controller.SaveSchedule(dto);

            _filmScheduleRepoMock.Verify(r => r.Add(It.IsAny<FilmSchedule>()), Times.Once);
            _filmScheduleRepoMock.Verify(r => r.Save(), Times.Once);
        }

        [Test]
        public void SaveSchedule_RedirectsToKinoSchedule()
        {
            var dto = new FilmScheduleDto { Id = Guid.NewGuid(), FilmId = _films[0].Id, KinoId = _kinos[0].Id, ProjectionDate = DateTime.Now.AddDays(1) };
            var result = _controller.SaveSchedule(dto) as RedirectToActionResult;
            Assert.That(result.ActionName, Is.EqualTo("KinoSchedule"));
            Assert.That(result.RouteValues["kinoId"], Is.EqualTo(dto.KinoId));
        }

        // ── DeleteSchedule ────────────────────────────────────────────────
        [Test]
        public void DeleteSchedule_ExistingSchedule_DeletesAndSaves()
        {
            var schedule = _schedules[0];
            _filmScheduleRepoMock.Setup(r => r.GetById<FilmSchedule>(schedule.Id)).Returns(schedule);

            _controller.DeleteSchedule(schedule.Id, _kinos[0].Id);

            _filmScheduleRepoMock.Verify(r => r.Delete(schedule), Times.Once);
            _filmScheduleRepoMock.Verify(r => r.Save(), Times.Once);
        }

        [Test]
        public void DeleteSchedule_RedirectsToKinoSchedule()
        {
            var schedule = _schedules[0];
            _filmScheduleRepoMock.Setup(r => r.GetById<FilmSchedule>(schedule.Id)).Returns(schedule);

            var result = _controller.DeleteSchedule(schedule.Id, _kinos[0].Id) as RedirectToActionResult;

            Assert.That(result.ActionName, Is.EqualTo("KinoSchedule"));
        }

        [Test]
        public void DeleteSchedule_NonExistingSchedule_ReturnsNotFound()
        {
            _filmScheduleRepoMock.Setup(r => r.GetById<FilmSchedule>(It.IsAny<Guid>())).Returns((FilmSchedule)null);

            var result = _controller.DeleteSchedule(Guid.NewGuid(), Guid.NewGuid());

            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        // ── Booked ────────────────────────────────────────────────────────
        [Test]
        public void Booked_NoFilmScheduleId_DoesNotBook()
        {
            _userRepoMock.Setup(r => r.GetById<AppUser>(_userId)).Returns(_user);
            _bookedFilmRepoMock
                .Setup(r => r.FindWithIncludes<BookedFilm>(It.IsAny<Expression<Func<BookedFilm, bool>>>(), It.IsAny<Expression<Func<BookedFilm, object>>[]>()))
                .Returns(new List<BookedFilm>().AsQueryable());

            _controller.Booked(Guid.Empty);

            _bookedFilmRepoMock.Verify(r => r.Add(It.IsAny<BookedFilm>()), Times.Never);
        }

        [Test]
        public void Booked_WithFilmScheduleId_AddsBooking()
        {
            var schedule = _schedules[0];
            _userRepoMock.Setup(r => r.GetById<AppUser>(_userId)).Returns(_user);
            _filmScheduleRepoMock.Setup(r => r.GetById<FilmSchedule>(schedule.Id)).Returns(schedule);
            _bookedFilmRepoMock
                .Setup(r => r.FindWithIncludes<BookedFilm>(It.IsAny<Expression<Func<BookedFilm, bool>>>(), It.IsAny<Expression<Func<BookedFilm, object>>[]>()))
                .Returns(new List<BookedFilm>().AsQueryable());

            _controller.Booked(schedule.Id);

            _bookedFilmRepoMock.Verify(r => r.Add(It.IsAny<BookedFilm>()), Times.Once);
            _bookedFilmRepoMock.Verify(r => r.Save(), Times.Once);
        }

        [Test]
        public void Booked_SetsUserEmailInViewBag()
        {
            _userRepoMock.Setup(r => r.GetById<AppUser>(_userId)).Returns(_user);
            _bookedFilmRepoMock
                .Setup(r => r.FindWithIncludes<BookedFilm>(It.IsAny<Expression<Func<BookedFilm, bool>>>(), It.IsAny<Expression<Func<BookedFilm, object>>[]>()))
                .Returns(new List<BookedFilm>().AsQueryable());

            var result = (ViewResult)_controller.Booked(Guid.Empty);

            Assert.That(_controller.ViewBag.UserEmail, Is.EqualTo("test@test.com"));
        }

        [Test]
        public void Booked_ReturnsViewWithBookedFilms()
        {
            var bookedFilm = new BookedFilm
            {
                Id = Guid.NewGuid(),
                UserId = _userId,
                FilmScheduleId = _schedules[0].Id,
                FilmSchedule = _schedules[0]
            };

            _userRepoMock.Setup(r => r.GetById<AppUser>(_userId)).Returns(_user);
            _bookedFilmRepoMock
                .Setup(r => r.FindWithIncludes<BookedFilm>(It.IsAny<Expression<Func<BookedFilm, bool>>>(), It.IsAny<Expression<Func<BookedFilm, object>>[]>()))
                .Returns(new List<BookedFilm> { bookedFilm }.AsQueryable());
            _filmRepoMock.Setup(r => r.GetById<Film>(_films[0].Id)).Returns(_films[0]);

            var result = (ViewResult)_controller.Booked(Guid.Empty);
            var model = result.Model as List<BookedFilmsViewModel>;

            Assert.That(model.Count, Is.EqualTo(1));
            Assert.That(model[0].FilmName, Is.EqualTo("Inception"));
        }
        [TearDown]
        public void TearDown()
        {
            _controller?.Dispose();
        }
    }
}