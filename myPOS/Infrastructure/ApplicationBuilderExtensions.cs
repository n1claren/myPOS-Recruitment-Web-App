using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using myPOS.Data;
using myPOS.Data.Models;

namespace myPOS.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        const string ADMIN_EMAIL = "Admin@myPOS.com";
        const string ADMIN_ROLE_NAME = "Admin";
        const string ADMIN_PHONE_NUMBER = "0876858618";
        const string ADMIN_PASSWORD = "Administrator13";

        public static IApplicationBuilder MigrateDatabase(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var provider = scope.ServiceProvider;

            var data = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            data.Database.Migrate();

            SeedAdministrator(provider);

            data.SaveChanges();

            return app;
        }

        private static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task.Run(async () =>
            {
                if (await roleManager.RoleExistsAsync(ADMIN_ROLE_NAME))
                {
                    return;
                }

                await roleManager.CreateAsync(new IdentityRole
                {
                    Name = ADMIN_ROLE_NAME
                });

                var user = new ApplicationUser
                {
                    Email = ADMIN_EMAIL,
                    UserName = ADMIN_ROLE_NAME,
                    PhoneNumber = ADMIN_PHONE_NUMBER
                };

                await userManager.CreateAsync(user, ADMIN_PASSWORD);

                await userManager.AddToRoleAsync(user, ADMIN_ROLE_NAME);
            })
            .GetAwaiter()
            .GetResult();
        }
    }
}
