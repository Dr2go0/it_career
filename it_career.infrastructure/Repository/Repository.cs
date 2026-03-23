using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using it_career.data;
using it_career.infrastructure.Interface;
namespace it_career.infrastructure.Repository
{
    public class Repository : IRepository
    {
        private ApplicationDbContext _context ;
        public Repository(ApplicationDbContext contex)
        {
            contex = _context;
        }


    }
}
