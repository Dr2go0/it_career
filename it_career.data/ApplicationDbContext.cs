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

            builder.Entity<BookedFilm>()
                .HasOne<AppUser>()
                .WithMany()
                .HasForeignKey(bf => bf.AppUserId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<BookedFilm>()
               .HasOne<FilmSchedule>()
               .WithMany()
                .HasForeignKey(bf => bf.FilmScheduleId)
                .OnDelete(DeleteBehavior.Restrict);

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
        DbSet<BookedFilm> BookedFilms { get; set; }

        } 
}
