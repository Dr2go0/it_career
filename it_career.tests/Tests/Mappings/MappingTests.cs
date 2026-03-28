using NUnit.Framework;
using it_career.data.models;
using it_career.infrastructure.Mappings;
using it_career.models;
using it_career.infrastructure.Extensions;

namespace it_career.tests.Mappings
{
    [TestFixture]
    public class MappingTests
    {
        // ── Film ──────────────────────────────────────────────────────────
        [Test]
        public void Film_ToDto_MapsAllFields()
        {
            var film = new Film
            {
                Id = Guid.NewGuid(),
                Name = "Inception",
                Genre = "Sci-Fi",
                Duration = 148,
                ReleaseDate = new DateTime(2010, 7, 16)
            };

            var dto = film.ToDto();

            Assert.That(dto.Id, Is.EqualTo(film.Id));
            Assert.That(dto.Name, Is.EqualTo(film.Name));
            Assert.That(dto.Genre, Is.EqualTo(film.Genre));
            Assert.That(dto.Duration, Is.EqualTo(film.Duration));
            Assert.That(dto.ReleaseDate, Is.EqualTo(film.ReleaseDate));
        }

        [Test]
        public void FilmDto_ToEntity_MapsAllFields()
        {
            var dto = new FilmDto
            {
                Id = Guid.NewGuid(),
                Name = "Inception",
                Genre = "Sci-Fi",
                Duration = 148,
                ReleaseDate = new DateTime(2010, 7, 16)
            };

            var entity = dto.ToEntity();

            Assert.That(entity.Id, Is.EqualTo(dto.Id));
            Assert.That(entity.Name, Is.EqualTo(dto.Name));
            Assert.That(entity.Genre, Is.EqualTo(dto.Genre));
            Assert.That(entity.Duration, Is.EqualTo(dto.Duration));
            Assert.That(entity.ReleaseDate, Is.EqualTo(dto.ReleaseDate));
        }

        [Test]
        public void Film_ToDto_ToEntity_RoundTrip_PreservesData()
        {
            var original = new Film
            {
                Id = Guid.NewGuid(),
                Name = "Matrix",
                Genre = "Action",
                Duration = 136,
                ReleaseDate = new DateTime(1999, 3, 31)
            };

            var result = original.ToDto().ToEntity();

            Assert.That(result.Id, Is.EqualTo(original.Id));
            Assert.That(result.Name, Is.EqualTo(original.Name));
        }

        // ── Kino ──────────────────────────────────────────────────────────
        [Test]
        public void Kino_ToDto_MapsAllFields()
        {
            var kino = new Kino
            {
                Id = Guid.NewGuid(),
                Name = "Odeon",
                Location = "Sofia",
                Capacity = 300
            };

            var dto = kino.ToDto();

            Assert.That(dto.Id, Is.EqualTo(kino.Id));
            Assert.That(dto.Name, Is.EqualTo(kino.Name));
            Assert.That(dto.Location, Is.EqualTo(kino.Location));
            Assert.That(dto.Capacity, Is.EqualTo(kino.Capacity));
        }

        [Test]
        public void KinoDto_ToEntity_MapsAllFields()
        {
            var dto = new KinoDto
            {
                Id = Guid.NewGuid(),
                Name = "Odeon",
                Location = "Sofia",
                Capacity = 300
            };

            var entity = dto.ToEntity();

            Assert.That(entity.Id, Is.EqualTo(dto.Id));
            Assert.That(entity.Name, Is.EqualTo(dto.Name));
            Assert.That(entity.Location, Is.EqualTo(dto.Location));
            Assert.That(entity.Capacity, Is.EqualTo(dto.Capacity));
        }

        // ── FilmSchedule ──────────────────────────────────────────────────
        [Test]
        public void FilmSchedule_ToDto_MapsAllFields()
        {
            var schedule = new FilmSchedule
            {
                Id = Guid.NewGuid(),
                FilmId = Guid.NewGuid(),
                KinoId = Guid.NewGuid(),
                ProjectionDate = DateTime.Now.AddDays(3)
            };

            var dto = schedule.ToDto();

            Assert.That(dto.Id, Is.EqualTo(schedule.Id));
            Assert.That(dto.FilmId, Is.EqualTo(schedule.FilmId));
            Assert.That(dto.KinoId, Is.EqualTo(schedule.KinoId));
            Assert.That(dto.ProjectionDate, Is.EqualTo(schedule.ProjectionDate));
        }

        [Test]
        public void FilmScheduleDto_ToEntity_MapsAllFields()
        {
            var dto = new FilmScheduleDto
            {
                Id = Guid.NewGuid(),
                FilmId = Guid.NewGuid(),
                KinoId = Guid.NewGuid(),
                ProjectionDate = DateTime.Now.AddDays(3)
            };

            var entity = dto.ToEntity();

            Assert.That(entity.Id, Is.EqualTo(dto.Id));
            Assert.That(entity.FilmId, Is.EqualTo(dto.FilmId));
            Assert.That(entity.KinoId, Is.EqualTo(dto.KinoId));
            Assert.That(entity.ProjectionDate, Is.EqualTo(dto.ProjectionDate));
        }

        // ── BookedFilm ────────────────────────────────────────────────────
        [Test]
        public void BookedFilm_ToDto_MapsAllFields()
        {
            var booked = new BookedFilm
            {
                Id = Guid.NewGuid(),
                UserId = "user-123",
                FilmScheduleId = Guid.NewGuid()
            };

            var dto = booked.ToDto();

            Assert.That(dto.Id, Is.EqualTo(booked.Id));
            Assert.That(dto.AppUserId, Is.EqualTo(booked.UserId));
            Assert.That(dto.FilmScheduleId, Is.EqualTo(booked.FilmScheduleId));
        }

        [Test]
        public void BookedFilmDto_ToEntity_MapsAllFields()
        {
            var dto = new BookedFilmDto
            {
                Id = Guid.NewGuid(),
                AppUserId = "user-123",
                FilmScheduleId = Guid.NewGuid()
            };

            var entity = dto.ToEntity();

            Assert.That(entity.Id, Is.EqualTo(dto.Id));
            Assert.That(entity.UserId, Is.EqualTo(dto.AppUserId));
            Assert.That(entity.FilmScheduleId, Is.EqualTo(dto.FilmScheduleId));
        }

        // ── AppUser ───────────────────────────────────────────────────────
        [Test]
        public void AppUser_ToDto_MapsEmailAndId()
        {
            var user = new AppUser
            {
                Id = "user-abc",
                Email = "test@test.com",
                UserName = "test@test.com"
            };

            var dto = user.ToDto();

            Assert.That(dto.Id, Is.EqualTo(user.Id));
            Assert.That(dto.Email, Is.EqualTo(user.Email));
        }
    }
}