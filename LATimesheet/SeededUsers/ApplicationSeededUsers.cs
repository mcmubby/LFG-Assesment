using LATimesheet.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace LATimesheet.API.SeededUsers
{
    public class ApplicationSeededUsers
    {
        public static async Task SeedEssentialsAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var adminRole = "Administrator";
            var userRole = "User";
            var password = "Password10##";

            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(adminRole));
            await roleManager.CreateAsync(new IdentityRole(userRole));

            //Seed Users
            var firstUser = new ApplicationUser { UserName = "LFG001", Email = "LFG001@lfg.com", PhoneNumber = "07010000000", EmailConfirmed = true, PhoneNumberConfirmed = true };
            var secondUser = new ApplicationUser { UserName = "LFG002", Email = "LFG002@lfg.com", PhoneNumber = "07020000000", EmailConfirmed = true, PhoneNumberConfirmed = true };
            var AdminUser = new ApplicationUser { UserName = "LFG-A001", Email = "LFG-A001@lfg.com", PhoneNumber = "07030000000", EmailConfirmed = true, PhoneNumberConfirmed = true };

            if (userManager.Users.All(u => (u.Email != firstUser.Email && u.Email != secondUser.Email && u.Email != AdminUser.Email)))
            {
                await userManager.CreateAsync(firstUser, password);
                await userManager.AddToRoleAsync(firstUser, userRole);

                await userManager.CreateAsync(secondUser, password);
                await userManager.AddToRoleAsync(secondUser, userRole);

                await userManager.CreateAsync(AdminUser, password);
                await userManager.AddToRoleAsync(AdminUser, adminRole);
            }
        }
    }
}
