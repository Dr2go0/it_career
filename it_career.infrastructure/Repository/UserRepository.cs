using it_career.data;
using it_career.infrastructure.Interface;
using it_career.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace it_career.infrastructure.Repository
{
    public class UserRepository : Repository, IUserRepository
    {
        private ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext contex)
            : base(contex)
        {
            _context = contex;
        }

        public IList<UserDTO> GetAllUsers()
        {
            
            List<UserDTO> userDTOs = _context.Users.Select(x => new UserDTO { Email = x.Email, Password = x.PasswordHash}).ToList();
            return userDTOs;
        }
    }
}
