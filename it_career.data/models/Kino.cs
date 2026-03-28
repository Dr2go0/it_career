using Microsoft.EntityFrameworkCore.Query.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace it_career.data.models
{
    public class Kino
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public int Capacity { get; set; }
        [Required]
        [ForeignKey(nameof(Manager))]
        public string ManagerId { get; set; } = null!;
        public AppUser Manager { get; set; } = null!;

        public List<FilmSchedule> FilmSchedules { get; set; } = new();

    }
}
