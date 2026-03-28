using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace it_career.models
{
    public class HomeIndexViewModel
    {
        public string UserId { get; set; }
        public List<KinoDto> Kinos { get; set; }
    }
}
