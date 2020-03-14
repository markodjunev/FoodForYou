namespace FoodForYou.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using FoodForYou.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (await dbContext.Categories.AnyAsync())
            {
                return;
            }

            var categories = new List<string> { "Beverages", "Salads", "Soups", "Pizza", "Pasta", "Pasta", "Burgers", "Seafood", "Meat", "Desserts", "Wine" };

            foreach (var category in categories)
            {
                await dbContext.Categories.AddAsync(new Category
                {
                    Name = category,
                });
            }
        }
    }
}
