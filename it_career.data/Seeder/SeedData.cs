using it_career.data.models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace it_career.data.Seeder
{
    public class SeedData
    {
        private UserManager<AppUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        public SeedData(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async void Create()
        {


            string[] roles = { "Admin", "User", "Manager" };

            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }
            if (!_userManager.Users.Any())
            {
                var admin = new AppUser
                {
                    UserName = "admin",
                    Email = "admin@gmail.com"

                };
                var res = await _userManager.CreateAsync(admin, "Admin!123");
                if (res.Succeeded)
                {
                    await _userManager.AddToRoleAsync(admin, "Admin");
                }
            }

        }
    }
}
