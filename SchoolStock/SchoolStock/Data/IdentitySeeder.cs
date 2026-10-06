using Microsoft.AspNetCore.Identity;

namespace SchoolStock.Data;

public static class IdentitySeeder
{
    public static async Task SeedAsync(
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        string[] roles =
        {
            "Admin",
            "Gestor",
            "User"
        };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(
                    new IdentityRole(role)
                );
            }
        }

        var adminEmail = "admin@schoolstock.com";

        var admin = await userManager.FindByEmailAsync(adminEmail);

        if (admin == null)
        {
            admin = new IdentityUser
            {
                UserName = "admin",
                Email = adminEmail,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(
                admin,
                "Admin@123"
            );

            if (!result.Succeeded)
            {
                throw new Exception(
                    string.Join(
                        "; ",
                        result.Errors.Select(e => e.Description)
                    )
                );
            }
        }

        if (!await userManager.IsInRoleAsync(admin, "Admin"))
        {
            await userManager.AddToRoleAsync(admin, "Admin");
        }

        if (await userManager.IsInRoleAsync(admin, "User"))
        {
            await userManager.RemoveFromRoleAsync(admin, "User");
        }
    }
}