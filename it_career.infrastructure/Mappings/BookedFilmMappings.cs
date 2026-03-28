using it_career.data.models;
using it_career.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace it_career.infrastructure.Extensions
{
    public static class BookedFilmMappings
    {
        public static BookedFilm ToEntity(this BookedFilmDto dto)
        {
            return new BookedFilm
            {
                Id = dto.Id ?? Guid.NewGuid(),
                UserId = dto.AppUserId,
                FilmScheduleId = dto.FilmScheduleId
            };
        }
        public static BookedFilmDto ToDto(this BookedFilm entity)
        {
            return new BookedFilmDto
            {
                Id = entity.Id,
                AppUserId = entity.UserId,
                FilmScheduleId = entity.FilmScheduleId
            };
        }
    }
}
