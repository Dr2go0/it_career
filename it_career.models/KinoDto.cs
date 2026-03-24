using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace it_career.models
{
    public class KinoDto
    {
        public string Location { get; set; }
        public Dictionary<DateTime, FilmDto>  a= new Dictionary<DateTime, FilmDto>();
    }
}
