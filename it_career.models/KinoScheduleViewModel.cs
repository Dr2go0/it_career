using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace it_career.models
{
    public class KinoScheduleViewModel
    {
        public List<FilmScheduleDto> FilmSchedules { get; set; }
        public List<FilmDto> Films { get; set; }
    }
}
