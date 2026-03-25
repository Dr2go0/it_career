using it_career.data.models;
using it_career.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace it_career.infrastructure.Mappings
{
    public static class UserMappings
    {
        public static UserDto ToDto(this AppUser user)
        {
            return new UserDto
            {
                Id = user.Id,
                Email = user.Email
                
            };
        }

        public static AppUser ToEntity(this UserDto dto)
        {
            return new AppUser
            {
                Id = dto.Id,
                Email = dto.Email
                
            };
        }
    }
}
