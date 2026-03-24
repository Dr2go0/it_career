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
        protected Dictionary<Kino, DateTime> BookedFilm = new Dictionary<Kino, DateTime>();
    }
}
