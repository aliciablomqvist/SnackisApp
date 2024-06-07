using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SnackisApp.Data;
using SnackisApp.Models;
using System.Linq;
using System.Threading.Tasks;
public class SeedData
{
    public static async Task Initialize(ApplicationDbContext context, UserManager<SnackisUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        context.Database.Migrate();

        if (!roleManager.Roles.Any())
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
            await roleManager.CreateAsync(new IdentityRole("User"));
        }

        var adminEmail = "admin@domain.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            adminUser = new SnackisUser { UserName = adminEmail, Email = adminEmail };
            await userManager.CreateAsync(adminUser, "Admin123!");
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
        else
        {
            var token = await userManager.GeneratePasswordResetTokenAsync(adminUser);
            var resetResult = await userManager.ResetPasswordAsync(adminUser, token, "Admin123!!");

            if (!resetResult.Succeeded)
            {
                foreach (var error in resetResult.Errors)
                {
                    Console.WriteLine(error.Description);
                }
            }
        }
    }
}
