using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace it_career.models
{
    public class BookedFilmDto
    {
        public Guid? Id { get; set; }
        public string AppUserId { get; set; }
        public Guid? FilmScheduleId { get; set; }
    }
}
