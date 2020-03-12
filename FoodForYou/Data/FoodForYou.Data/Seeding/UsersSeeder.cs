namespace FoodForYou.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FoodForYou.Common;
    using FoodForYou.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class UsersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Users.Any())
            {
                return;
            }

            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var adminUser = new ApplicationUser
            {
                FirstName = "Ivan",
                LastName = "Ivanov",
                Address = "ul. Tintyava 500",
                PhoneNumber = "+359 123 456 780",
                UserName = "admin",
                Email = "admin.a@abv.bg",
            };

            var user = new ApplicationUser
            {
                FirstName = "Georgi",
                LastName = "Georgiev",
                Address = "ul. Moscovska 55",
                PhoneNumber = "+359 023 456 780",
                UserName = "Goshoo",
                Email = "gosho.g@abv.bg",
            };

            await SeedUserAsync(userManager, adminUser);
            await SeedUserAsync(userManager, user);

            // await userManager.AddToRoleAsync(adminUser, GlobalConstants.AdministratorRoleName);
            await userManager.AddToRoleAsync(user, GlobalConstants.UserRoleName);
        }

        private static async Task SeedUserAsync(UserManager<ApplicationUser> userManager, ApplicationUser user)
        {
            var userExist = await userManager.FindByNameAsync(user.UserName);
            if (userExist == null)
            {
                var result = await userManager.CreateAsync(user, "123456");
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
