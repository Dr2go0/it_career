namespace it_career.Models
{
    public class FilmDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public int Duration { get; set; }
        public DateTime ReleaseDate { get; set; }
        public FilmDto(string Name, string Genre, int Duration, DateTime ReleaseDate)
        {
            this.Name = Name;
            this.Genre = Genre;
            this.Duration = Duration;
            this.ReleaseDate = ReleaseDate;
            Id =  Guid.NewGuid();
        }
    }
}
