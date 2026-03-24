using System.ComponentModel.DataAnnotations;

namespace it_career.data.models
{
    public class Film
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Genre { get; set; }
        public int Duration { get; set; }
        public DateTime ReleaseDate { get; set; }

        public List<FilmSchedule> Schedules { get; set; } = new();

    }
}
