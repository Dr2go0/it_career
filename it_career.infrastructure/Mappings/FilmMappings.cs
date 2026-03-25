using it_career.data.models;
using it_career.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace it_career.infrastructure.Extensions
{
    public static class FilmMappings
    {
       
        public static Film ToEntity(this FilmDto dto)
        {
            return new Film
            {
                Id = dto.Id,
                Name = dto.Name,
                Genre = dto.Genre,
                Duration = dto.Duration,
                ReleaseDate = dto.ReleaseDate
            };
        }

        public static FilmDto ToDto(this Film entity)
        {
            return new FilmDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Genre = entity.Genre,
                Duration = entity.Duration,
                ReleaseDate = entity.ReleaseDate
            };
        }
    }
}
