using NUnit.Framework;
using it_career.data.models;
using System.ComponentModel.DataAnnotations;

namespace it_career.tests.Models
{
    [TestFixture]
    public class ModelValidationTests
    {
        private List<ValidationResult> ValidateModel(object model)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(model);
            Validator.TryValidateObject(model, context, results, true);
            return results;
        }

        // ── Film ──────────────────────────────────────────────────────────
        [Test]
        public void Film_ValidModel_PassesValidation()
        {
            var film = new Film
            {
                Id = Guid.NewGuid(),
                Name = "Inception",
                Genre = "Sci-Fi",
                Duration = 148,
                ReleaseDate = new DateTime(2010, 7, 16)
            };
            Assert.That(ValidateModel(film), Is.Empty);
        }

        [Test]
        public void Film_MissingName_FailsValidation()
        {
            var film = new Film { Id = Guid.NewGuid(), Genre = "Action", Duration = 120 };
            Assert.That(ValidateModel(film), Is.Not.Empty);
        }

        [Test]
        public void Film_SchedulesInitializedEmpty()
        {
            var film = new Film();
            Assert.That(film.Schedules, Is.Not.Null);
            Assert.That(film.Schedules, Is.Empty);
        }

        // ── Kino ──────────────────────────────────────────────────────────
       
      
        [Test]
        public void Kino_MissingName_FailsValidation()
        {
            var kino = new Kino { Location = "Sofia", Capacity = 100 };
            Assert.That(ValidateModel(kino), Is.Not.Empty);
        }

        [Test]
        public void Kino_MissingLocation_FailsValidation()
        {
            var kino = new Kino { Name = "Odeon", Capacity = 100 };
            Assert.That(ValidateModel(kino), Is.Not.Empty);
        }

        [Test]
        public void Kino_FilmSchedulesInitializedEmpty()
        {
            var kino = new Kino();
            Assert.That(kino.FilmSchedules, Is.Not.Null);
            Assert.That(kino.FilmSchedules, Is.Empty);
        }

        // ── FilmSchedule ──────────────────────────────────────────────────
        [Test]
        public void FilmSchedule_ValidModel_HasCorrectRelations()
        {
            var filmId = Guid.NewGuid();
            var kinoId = Guid.NewGuid();
            var schedule = new FilmSchedule
            {
                Id = Guid.NewGuid(),
                FilmId = filmId,
                KinoId = kinoId,
                ProjectionDate = DateTime.Now.AddDays(5)
            };
            Assert.That(schedule.FilmId, Is.EqualTo(filmId));
            Assert.That(schedule.KinoId, Is.EqualTo(kinoId));
        }

        // ── BookedFilm ────────────────────────────────────────────────────
        [Test]
        public void BookedFilm_ValidModel_HasCorrectFKs()
        {
            var scheduleId = Guid.NewGuid();
            var booked = new BookedFilm
            {
                Id = Guid.NewGuid(),
                UserId = "user-123",
                FilmScheduleId = scheduleId
            };
            Assert.That(booked.UserId, Is.EqualTo("user-123"));
            Assert.That(booked.FilmScheduleId, Is.EqualTo(scheduleId));
        }

        [Test]
        public void BookedFilm_FilmScheduleId_CanBeNull()
        {
            var booked = new BookedFilm { Id = Guid.NewGuid(), UserId = "user-123" };
            Assert.That(booked.FilmScheduleId, Is.Null);
        }
    }
}