using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using it_career.models;
namespace it_career.infrastructure.Interface
{
    public interface IUserRepository
    {
        IList<UserDTO> GetAllUsers();
    }
}
