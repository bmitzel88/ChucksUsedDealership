using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;

namespace ChucksUsedDealership.Models
{
#nullable disable
    public static class IdentityHelper
    {
        public const string Admin = "Admin";
        public const string User = "User";

        public static async Task CreateRoles(IServiceProvider provider, params string[] roles)
        {
            RoleManager<IdentityRole> roleManager = provider.GetService<RoleManager<IdentityRole>>();

            foreach (string role in roles)
            {
                bool doesRoleExist = await roleManager.RoleExistsAsync(role);

                if (!doesRoleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

        }


        public static async Task CreateDefaultUser(IServiceProvider provider, string role)
        {
            var userManager = provider.GetService<UserManager<IdentityUser>>();

            // If no users are present make the default user
            int numUsers = (await userManager.GetUsersInRoleAsync(role)).Count;
            
            // If the no users are in the specified role
            if (numUsers == 0)
            {
                var defaultAdmin = new IdentityUser()
                {
                    Email = "admin@chucksdealership.com",
                    UserName = "admin"
                };

                // Create the user with a password
                var result = await userManager.CreateAsync(defaultAdmin, "Admin123!");

                if (result.Succeeded)
                {
                    // Assign the default admin to the specified role
                    await userManager.AddToRoleAsync(defaultAdmin, role);
                }
                else
                {
                    // Handle potential errors during user creation
                    throw new Exception("Failed to create the default admin user.");
                }

            }

        }
    }
}
