using Garage_3._0.Models;
using Microsoft.AspNetCore.Identity;

namespace Garage_3._0.Data
{
    public class SeedData
    {
        private static ApplicationDbContext context = default!;
        private static RoleManager<IdentityRole> roleManager = default!;
        private static UserManager<ApplicationUser> userManager = default!;

        public static async Task Init(ApplicationDbContext _context, IServiceProvider services)
        {
            context = _context;
            if (context.Roles.Any()) return;
            roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleNames = new[] { "Member", "Admin" };
            var adminEmail = "admin@Garage.se";
            var userEmail = "user@user.com";
            await AddRolesAsync(roleNames);
            var admin = await AddAccountAsync(adminEmail, "Admin", "Adminsson", "P@55w.rd", "199001015678");
            var user = await AddAccountAsync(userEmail, "User", "Usersson", "Pa55w.rd", "199502145432");
            await AddUserToRoleAsync(admin, "Admin");
            await AddUserToRoleAsync(user, "Member");
        }
        private static async Task AddUserToRoleAsync(ApplicationUser user, string roleName)
        {
            if (!await userManager.IsInRoleAsync(user, roleName))
            {
                var result = await userManager.AddToRoleAsync(user, roleName);
                if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));
            }
        }
        private static async Task AddRolesAsync(string[] roleNames)
        {
            foreach (var roleName in roleNames)
            {
                if (await roleManager.RoleExistsAsync(roleName)) continue;
                var role = new IdentityRole { Name = roleName };
                var result = await roleManager.CreateAsync(role);
                if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));
            }
        }
        private static async Task<ApplicationUser> AddAccountAsync(string accountEmail, string fName, string lName, string pw, string ssn)
        {
            var found = await userManager.FindByEmailAsync(accountEmail);
            if (found != null) return null!;
            var user = new ApplicationUser
            {
                UserName = accountEmail,
                Email = accountEmail,
                FirstName = fName,
                LastName = lName,
                SSN = ssn,
                EmailConfirmed = true
            };
            var result = await userManager.CreateAsync(user, pw);
            if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));
            return user;
        }
    }
}
