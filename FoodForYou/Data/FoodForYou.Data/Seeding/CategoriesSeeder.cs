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
                ImageUrl = "https://res.cloudinary.com/dzheivzqv/image/upload/v1584538400/FoodForYou/beverages_k8wmhz.jpg",
            });

            await dbContext.Categories.AddAsync(new Category
            {
                Name = "Salads",
                ImageUrl = "https://res.cloudinary.com/dzheivzqv/image/upload/v1584538413/FoodForYou/salads_g85ypa.jpg",
            });

            await dbContext.Categories.AddAsync(new Category
            {
                Name = "Soups",
                ImageUrl = "https://res.cloudinary.com/dzheivzqv/image/upload/v1584538400/FoodForYou/soups_azypsi.jpg",
            });

            await dbContext.Categories.AddAsync(new Category
            {
                Name = "Pizza",
                ImageUrl = "https://res.cloudinary.com/dzheivzqv/image/upload/v1584538413/FoodForYou/pizzas_jfag31.jpg",
            });

            await dbContext.Categories.AddAsync(new Category
            {
                Name = "Pasta",
                ImageUrl = "https://res.cloudinary.com/dzheivzqv/image/upload/v1584538413/FoodForYou/pasta_njf3d6.jpg",
            });

            await dbContext.Categories.AddAsync(new Category
            {
                Name = "Burgers",
                ImageUrl = "https://res.cloudinary.com/dzheivzqv/image/upload/v1584538400/FoodForYou/burgers_w0aany.jpg",
            });

            await dbContext.Categories.AddAsync(new Category
            {
                Name = "Seafood",
                ImageUrl = "https://res.cloudinary.com/dzheivzqv/image/upload/v1584538413/FoodForYou/seafood_goo2v4.jpg",
            });

            await dbContext.Categories.AddAsync(new Category
            {
                Name = "Meat",
                ImageUrl = "https://res.cloudinary.com/dzheivzqv/image/upload/v1584538400/FoodForYou/meat_loc9rq.jpg",
            });

            await dbContext.Categories.AddAsync(new Category
            {
                Name = "Desserts",
                ImageUrl = "https://res.cloudinary.com/dzheivzqv/image/upload/v1584538400/FoodForYou/Desserts_mavelg.jpg",
            });

            await dbContext.Categories.AddAsync(new Category
            {
                Name = "Wine",
                ImageUrl = "https://res.cloudinary.com/dzheivzqv/image/upload/v1584538400/FoodForYou/wine_aa5fbn.jpg",
            });
        }
    }
}
