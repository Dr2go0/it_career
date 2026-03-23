using it_career.data.models;
using it_career.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace it_career.data.models
{
    public class AppUser : IdentityUser
    {
        protected Dictionary<KinoDto, DateTime> BookedFilm = new Dictionary<KinoDto, DateTime>();
        public AppUser()
        {
            
        }
        public void BookFIlm(KinoDto Kino, DateTime DateTime)
        {

            BookedFilm.Add(Kino, DateTime);
        }
    }
}
