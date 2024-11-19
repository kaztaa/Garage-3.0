using Garage_3._0.Models;
using Microsoft.AspNetCore.Identity;

namespace Garage_3._0.Data
{
    public class SeedData
    {
        public static async Task Init(ApplicationDbContext context, IServiceProvider services)
        {
            if (context.Roles.Any()) return;

            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

            var roleNames = new[] { "Member", "Admin" };
            var adminEmail = "admin@Garage.se";
            var userEmail = "user@user.com";

            await AddRolesAsync(roleManager, roleNames);
            var admin = await AddAccountAsync(userManager, adminEmail, "Admin", "Adminsson", "P@55w.rd");
            var user = await AddAccountAsync(userManager, userEmail, "User", "Usersson", "Pa55w.rd");

            await AddUserToRoleAsync(userManager, admin, "Admin");
            await AddUserToRoleAsync(userManager, user, "Member");
        }

        private static async Task AddUserToRoleAsync(UserManager<ApplicationUser> userManager, ApplicationUser user, string roleName)
        {
            if (!await userManager.IsInRoleAsync(user, roleName))
            {
                var result = await userManager.AddToRoleAsync(user, roleName);
                if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));
            }
        }

        private static async Task AddRolesAsync(RoleManager<IdentityRole> roleManager, string[] roleNames)
        {
            foreach (var roleName in roleNames)
            {
                if (await roleManager.RoleExistsAsync(roleName)) continue;
                var role = new IdentityRole { Name = roleName };
                var result = await roleManager.CreateAsync(role);
                if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));
            }
        }

        private static async Task<ApplicationUser> AddAccountAsync(UserManager<ApplicationUser> userManager, string accountEmail, string fName, string lName, string pw)
        {
            var found = await userManager.FindByEmailAsync(accountEmail);
            if (found != null) return null!;

            var user = new ApplicationUser
            {
                UserName = accountEmail,
                Email = accountEmail,
                FirstName = fName,
                LastName = lName,
                EmailConfirmed = true
            };
            var result = await userManager.CreateAsync(user, pw);
            if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));

            return user;
        }
    }
}
