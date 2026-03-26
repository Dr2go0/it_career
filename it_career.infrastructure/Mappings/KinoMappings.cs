using it_career.data.models;
using it_career.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace it_career.infrastructure.Mappings
{
    public static class KinoMappings
    {
        public static KinoDto ToDto(this Kino kino)
        {
            return new KinoDto
            {
                Id = kino.Id,
                Name = kino.Name,
                Location = kino.Location,
                Capacity = kino.Capacity
            };
        }

        public static Kino ToEntity(this KinoDto dto)
        {
            return new Kino
            {
                Name = dto.Name,
                Location = dto.Location,
                Capacity = dto.Capacity
            };
        }
    }
}
