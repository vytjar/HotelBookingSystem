using HotelManagementSystem.Interfaces.Constants;
using HotelManagementSystem.Interfaces.Entities;
using Microsoft.AspNetCore.Identity;

namespace HotelManagementSystem.Services.Repositories
{
    public class AuthSeeder(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
    {
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
        private readonly UserManager<User> _userManager = userManager;

        public async Task SeedAsync()
        {
            await AddDefaultRolesAsync();

            await AddAdminUserAsync();
        }

        public async Task AddAdminUserAsync()
        {
            if (await _userManager.FindByNameAsync("admin") is null)
            {
                var user = new User
                {
                    Email = "admin@admin.com",
                    Name = "admin",
                    Surname = "admin",
                    UserName = "admin"
                };

                var result = await _userManager.CreateAsync(user, "Test@1234");

                if (result.Succeeded)
                {
                    await _userManager.AddToRolesAsync(user, Roles.All);
                }
            }
        }

        public async Task AddDefaultRolesAsync()
        {
            foreach (var role in Roles.All)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
