
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace it_career.data.models
{
    public class FilmSchedule
    {
        [Key]
        public Guid Id { get; set; }

        public Guid FilmId { get; set; }    
        public Film Film { get; set; }

        public Guid KinoId { get; set; }     
        public Kino Kino { get; set; }

        public DateTime ProjectionDate { get; set; }

    }
}
