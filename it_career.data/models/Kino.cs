using Microsoft.EntityFrameworkCore.Query.Internal;

namespace it_career.Models
{
    public class Kino
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
        private Dictionary<Film, DateTime> filmSchedule = new Dictionary<Film, DateTime>();
        public Dictionary<Film, DateTime> FilmSchedule
        {
            get { return filmSchedule; }
        }
        public Kino(string Name, string Location, int Capacity)
        {
            this.Id = Guid.NewGuid();
            this.Name = Name;
            this.Location = Location;
            this.Capacity = Capacity;
        }
    }
}
