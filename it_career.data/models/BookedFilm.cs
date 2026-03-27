using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace it_career.data.models
{
    public class BookedFilm
    {
        [Key]
        public Guid Id { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public Guid FilmScheduleId { get; set; }
        public FilmSchedule FilmSchedule { get; set; }
    }
}
