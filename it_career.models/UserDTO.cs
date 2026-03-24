
namespace it_career.models
{
    public class UserDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public  Dictionary<KinoDto, DateTime> BookedFilm { get; set; } = new Dictionary<KinoDto, DateTime>();
        
    }
}
