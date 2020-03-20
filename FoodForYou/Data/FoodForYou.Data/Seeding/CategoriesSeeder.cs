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

            await dbContext.Categories.AddAsync(new Category
            {
                Name = "Beverages",
                ImageUrl = "https://res.cloudinary.com/dzheivzqv/image/upload/v1584695419/FoodForYou/beverages_dsviye.jpg",
            });

            await dbContext.Categories.AddAsync(new Category
            {
                Name = "Salads",
                ImageUrl = "https://res.cloudinary.com/dzheivzqv/image/upload/v1584695428/FoodForYou/salads_ydjdzp.jpg",
            });

            await dbContext.Categories.AddAsync(new Category
            {
                Name = "Soups",
                ImageUrl = "https://res.cloudinary.com/dzheivzqv/image/upload/v1584695419/FoodForYou/soups_tc7nfu.jpg",
            });

            await dbContext.Categories.AddAsync(new Category
            {
                Name = "Pizza",
                ImageUrl = "https://res.cloudinary.com/dzheivzqv/image/upload/v1584695428/FoodForYou/pizzas_ilrdd2.jpg",
            });

            await dbContext.Categories.AddAsync(new Category
            {
                Name = "Pasta",
                ImageUrl = "https://res.cloudinary.com/dzheivzqv/image/upload/v1584538413/FoodForYou/pasta_njf3d6.jpg",
            });

            await dbContext.Categories.AddAsync(new Category
            {
                Name = "Burgers",
                ImageUrl = "https://res.cloudinary.com/dzheivzqv/image/upload/v1584695434/FoodForYou/burgers_ha6dlp.jpg",
            });

            await dbContext.Categories.AddAsync(new Category
            {
                Name = "Seafood",
                ImageUrl = "https://res.cloudinary.com/dzheivzqv/image/upload/v1584695428/FoodForYou/seafood_zcgnmr.jpg",
            });

            await dbContext.Categories.AddAsync(new Category
            {
                Name = "Meat",
                ImageUrl = "https://res.cloudinary.com/dzheivzqv/image/upload/v1584695428/FoodForYou/meat_uj1o9l.jpg",
            });

            await dbContext.Categories.AddAsync(new Category
            {
                Name = "Desserts",
                ImageUrl = "https://res.cloudinary.com/dzheivzqv/image/upload/v1584695428/FoodForYou/Desserts_t6546o.jpg",
            });

            await dbContext.Categories.AddAsync(new Category
            {
                Name = "Wine",
                ImageUrl = "https://res.cloudinary.com/dzheivzqv/image/upload/v1584695428/FoodForYou/wine_parsql.jpg",
            });
        }
    }
}
