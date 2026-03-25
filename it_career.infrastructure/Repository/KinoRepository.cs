using it_career.data;
using it_career.infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace it_career.infrastructure.Repository
{
    public class KinoRepository : Repository, IKinoRepository
    {
        private ApplicationDbContext _context;
        public KinoRepository(ApplicationDbContext contex) : base(contex)
        {
            _context = contex;
        }
    }
}
