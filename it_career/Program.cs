using it_career.data;
using it_career.data.models;
using it_career.data.Seeder;
using it_career.infrastructure.Interface;
using it_career.infrastructure.Repository;
using it_career.models;
using it_career.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
namespace it_career
{
    public class Program
    {
        public  static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            // ✅ Registers UserManager, SignInManager, RoleManager + Authentication
            builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false; // set true if email confirmation is needed
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddScoped<IRepository, Repository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IFilmScheduleRepository, FilmScheduleRepository>();
            builder.Services.AddScoped<IFilmRepository, FilmRepository>();
            builder.Services.AddScoped<IKinoRepository, KinoRepository>();
            builder.Services.AddScoped<SeedData>();

            builder.Services.AddScoped<IBookedFilmRepository, BookedFilmRepository>();
            builder.Services.AddTransient<Microsoft.AspNetCore.Identity.UI.Services.IEmailSender, DummyEmailSender>();
            
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
                var seedData = app.Services.CreateScope().ServiceProvider.GetRequiredService<SeedData>();
                seedData.Create();

            app.UseHttpsRedirection();
                app.UseStaticFiles();

                app.UseRouting();
                app.UseAuthentication();
                app.UseAuthorization();

                app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                app.MapRazorPages();

                app.Run();
            }
        }
    }

