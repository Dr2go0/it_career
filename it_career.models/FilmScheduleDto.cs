using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace it_career.models
{
    public class FilmScheduleDto
    {
        public Guid? Id { get; set; }

        public Guid FilmId { get; set; }
        
        public Guid KinoId { get; set; }

        public DateTime ProjectionDate { get; set; }
    }
}
