using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace DTour.Infrastructure.Services;

public class DatabaseSeeder(UserManager<CustomUser> userManager, RoleManager<CustomRole> roleManager)
    : IDatabaseSeeder
{
    public void Initialize()
    {
        AddAdministrator();
    }

    private void AddAdministrator()
    {
        Task.Run(async () =>
        {
            var adminRole = new CustomRole()
            {
                Name = RoleConstants.AdministratorRole,
            };
            var adminRoleInDb = await roleManager.FindByNameAsync(adminRole.Name);
            if (adminRoleInDb == null)
            {
                await roleManager.CreateAsync(adminRole);
                var superUser = new CustomUser()
                {
                    UserName = "ngotuananh77@gmail.com",
                    Email = "ngotuananh77@gmail.com",
                    EmailConfirmed = true,
                    Name = "Ngo Tuan Anh"
                };
                var superUserInDb = await userManager.FindByEmailAsync(superUser.Email);
                if (superUserInDb == null)
                {
                    await userManager.CreateAsync(superUser, PasswordDefault.DefaulAdminPassword);
                    await userManager.AddToRoleAsync(superUser, adminRole.Name);
                    await userManager.AddClaimAsync(superUser, new Claim("DisplayName", "Ngo Tuan Anh"));
                }
            }
        }).GetAwaiter().GetResult();
    }
}