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
        public string Id { get; set; }

        public string FilmId { get; set; }
        
        public string KinoId { get; set; }

        public DateTime ProjectionDate { get; set; }
    }
}
