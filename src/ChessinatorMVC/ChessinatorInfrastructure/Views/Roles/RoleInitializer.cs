using ChessinatorDomain.Model;
using Microsoft.AspNetCore.Identity;

namespace ChessinatorInfrastructure.Views.Roles
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            string adminEmail = configuration["AdminSettings:Email"] ?? "default_admin@gmail.com";
            string password = configuration["AdminSettings:Password"] ?? "default_password";


            string[] roles = { "admin", "player", "organizer" };

            foreach (var role in roles)
            {
                if (await roleManager.RoleExistsAsync(role) == false)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                User admin = new User { Email = adminEmail, UserName = adminEmail };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }

    }

}
