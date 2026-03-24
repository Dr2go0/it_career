using Microsoft.EntityFrameworkCore.Query.Internal;
using System.ComponentModel.DataAnnotations;

namespace it_career.Models
{
    public class Kino
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public int Capacity { get; set; }
        private Dictionary<Film, DateTime> filmSchedule = new Dictionary<Film, DateTime>();

    }
}
