using Microsoft.AspNetCore.Identity;

namespace OnlineKutuphane.Utility
{
    public class SeedData
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync(UserRoles.Role_Admin))
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Role_Admin));
            }

            if (!await roleManager.RoleExistsAsync(UserRoles.Role_Ogrenci))
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Role_Ogrenci));
            }
        }

        public static async Task SeedUsersAsync(UserManager<IdentityUser> userManager)
        {
            // Admin kullanıcısı
            var adminUser = await userManager.FindByEmailAsync("admin@example.com");
            if (adminUser == null)
            {
                adminUser = new IdentityUser
                {
                    UserName = "admin@example.com",
                    Email = "admin@example.com",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(adminUser, "AdminPassword123!");
                await userManager.AddToRoleAsync(adminUser, UserRoles.Role_Admin);
            }

            // Öğrenci kullanıcısı
            var ogrenciUser = await userManager.FindByEmailAsync("ogrenci@example.com");
            if (ogrenciUser == null)
            {
                ogrenciUser = new IdentityUser
                {
                    UserName = "ogrenci@example.com",
                    Email = "ogrenci@example.com",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(ogrenciUser, "OgrenciPassword123!");
                await userManager.AddToRoleAsync(ogrenciUser, UserRoles.Role_Ogrenci);
            }
        }
    }
}
