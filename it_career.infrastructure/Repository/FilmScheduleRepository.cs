using it_career.data;
using it_career.infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace it_career.infrastructure.Repository
{
    public class FilmScheduleRepository : Repository, IFilmScheduleRepository
    {
        private ApplicationDbContext _context;
        public FilmScheduleRepository(ApplicationDbContext contex) 
            : base(contex)
        {
            _context = contex;
        }
    }
}
