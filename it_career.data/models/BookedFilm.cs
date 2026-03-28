using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace it_career.data.models
{
    public class BookedFilm
    {
        [Key]
        public Guid Id { get; set; }

        public string UserId { get; set; } = null!;
        [ForeignKey(nameof(UserId))]
        public AppUser User { get; set; } = null!;

        public Guid? FilmScheduleId { get; set; }
        [ForeignKey(nameof(FilmScheduleId))]
        public FilmSchedule FilmSchedule { get; set; } = null!;
    }
}
