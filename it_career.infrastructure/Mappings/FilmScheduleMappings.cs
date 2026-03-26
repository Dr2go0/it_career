using it_career.data.models;
using it_career.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace it_career.infrastructure.Mappings
{
    public static class FilmScheduleMappings
    {
        public static FilmScheduleDto ToDto(this FilmSchedule filmSchedule)
        {
            return new FilmScheduleDto
            {
                Id = filmSchedule.Id,
                FilmId = filmSchedule.FilmId,
                KinoId = filmSchedule.KinoId,
                ProjectionDate = filmSchedule.ProjectionDate
            };
        }

        public static FilmSchedule ToEntity(this FilmScheduleDto dto)
        {
            return new FilmSchedule
            {
                Id = dto.Id ?? Guid.NewGuid(),
                FilmId = dto.FilmId,
                KinoId = dto.KinoId,
                ProjectionDate = dto.ProjectionDate
            };
        }
    }
}
