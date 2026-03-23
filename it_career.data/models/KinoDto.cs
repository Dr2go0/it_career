using Microsoft.EntityFrameworkCore.Query.Internal;

namespace it_career.Models
{
    public class KinoDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
        private Dictionary<FilmDto, DateTime> filmSchedule = new Dictionary<FilmDto, DateTime>();
        public Dictionary<FilmDto, DateTime> FilmSchedule
        {
            get { return filmSchedule; }
        }
        public KinoDto(string Name, string Location, int Capacity)
        {
            this.Id = Guid.NewGuid();
            this.Name = Name;
            this.Location = Location;
            this.Capacity = Capacity;
        }
    }
}
