namespace FoodForYou.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using FoodForYou.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class ProductsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (await dbContext.Products.AnyAsync())
            {
                return;
            }

            await dbContext.Products.AddAsync(new Product
            {
                Name = "Caffe Espresso",
                Price = 1,
                Description = "200ml",
                CategoryId = 25,
                ImageUrl = "https://res.cloudinary.com/dzheivzqv/image/upload/v1584706164/FoodForYou/CaffeEspresso_clf4lj.jpg",
            });

            await dbContext.Products.AddAsync(new Product
            {
                Name = "Coca-Cola",
                Price = 1.50m,
                Description = "330ml",
                CategoryId = 25,
                ImageUrl = "https://res.cloudinary.com/dzheivzqv/image/upload/v1584706164/FoodForYou/Coca-Cola_qet7bq.jpg",
            });

            await dbContext.Products.AddAsync(new Product
            {
                Name = "Fanta Grape",
                Price = 1.5m,
                Description = "330ml",
                CategoryId = 25,
                ImageUrl = "https://res.cloudinary.com/dzheivzqv/image/upload/v1584706164/FoodForYou/FantaGrape_vn7mqx.jpg",
            });

            await dbContext.Products.AddAsync(new Product
            {
                Name = "Home Lemonade",
                Price = 7,
                Description = "1.5l",
                CategoryId = 25,
                ImageUrl = "https://res.cloudinary.com/dzheivzqv/image/upload/v1584706164/FoodForYou/HomeLemonade_ch4rxb.jpg",
            });

            await dbContext.Products.AddAsync(new Product
            {
                Name = "Tea",
                Price = 0.8m,
                Description = "150ml",
                CategoryId = 25,
                ImageUrl = "https://res.cloudinary.com/dzheivzqv/image/upload/v1584706165/FoodForYou/tea_yv8x0x.jpg",
            });

            await dbContext.Products.AddAsync(new Product
            {
                Name = "Water",
                Price = 0.7m,
                Description = "500ml",
                CategoryId = 25,
                ImageUrl = "https://res.cloudinary.com/dzheivzqv/image/upload/v1584706164/FoodForYou/Water_hjxwpz.jpg",
            });
        }
    }
}
