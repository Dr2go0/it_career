using it_career.data.models;
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

            builder.Entity<AppUser>()
        .HasMany(u => u.BookedFilms)
        .WithMany()
        .UsingEntity<Dictionary<string, object>>(
            "UserBookedFilms",                       
            j => j.HasOne<Film>()
                  .WithMany()
                  .HasForeignKey("FilmId")
                  .OnDelete(DeleteBehavior.Cascade),
            j => j.HasOne<AppUser>()
                  .WithMany()
                  .HasForeignKey("AppUserId")
                  .OnDelete(DeleteBehavior.Cascade)
        );

        
            builder.Entity<FilmSchedule>()
                .HasOne(fs => fs.Film)
                .WithMany(f => f.Schedules)
                .HasForeignKey(fs => fs.FilmId)
                .OnDelete(DeleteBehavior.Restrict);

           
            builder.Entity<FilmSchedule>()
                .HasOne(fs => fs.Kino)
                .WithMany(k => k.FilmSchedules)
                .HasForeignKey(fs => fs.KinoId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        DbSet<AppUser> AppUsers { get; set; }
        DbSet<Film> Film { get; set; }
        DbSet<Kino> Kino { get; set; }
        DbSet<FilmSchedule> FilmSchedule { get; set; }

    } 
}
