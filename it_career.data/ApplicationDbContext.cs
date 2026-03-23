using it_career.data.models;
using it_career.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace it_career.data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected ApplicationDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        DbSet<AppUser> AppUsers { get; set; }  
        DbSet<Film> Film {  get; set; }
        DbSet<Kino> Kino { get; set; }
        }
}
