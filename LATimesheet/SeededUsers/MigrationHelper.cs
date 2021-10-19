using System;
using System.Linq;
using System.Threading.Tasks;
using LATimesheet.Data.DbContexts;
using LATimesheet.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LATimesheet.SeededUsers
{
    public static class MigrationHelper
    {
        public static void MigrateDatabaseContext(IServiceProvider svp)
        {
            var applicationDbContext = svp.GetRequiredService<ApplicationDbContext>();
            applicationDbContext.Database.Migrate();
        }

        public static async Task SeedEssentialsAsync(IServiceProvider svp)
        {
            var userManager = svp.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = svp.GetRequiredService<RoleManager<IdentityRole>>();

            var adminRole = "Administrator";
            var userRole = "User";
            var password = "Password10##";

            //Seed Roles
           await roleManager.CreateAsync(new IdentityRole(adminRole));
           await roleManager.CreateAsync(new IdentityRole(userRole));

            //Create Default Users
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