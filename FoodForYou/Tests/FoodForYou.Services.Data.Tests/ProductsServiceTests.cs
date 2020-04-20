namespace FoodForYou.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FoodForYou.Data;
    using FoodForYou.Data.Common.Repositories;
    using FoodForYou.Data.Models;
    using FoodForYou.Data.Repositories;
    using FoodForYou.Services.Data;
    using FoodForYou.Services.Interfaces;
    using FoodForYou.Web.ViewModels.Categories;
    using FoodForYou.Web.ViewModels.Products;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class ProductsServiceTests
    {
        [Fact]
        public async Task ProductShouldBeCreateAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var dbContext = new ApplicationDbContext(options);

            var repository = new EfDeletableEntityRepository<Product>(dbContext);
            var service = new ProductService(repository);

            var product = new Product
            {
                CategoryId = 1,
                Name = "Tiramisu",
                Description = "450g.",
                Price = 5.5m,
                ImageUrl = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.dailymail.co.uk%2Fhealth%2Farticle-7469271%2FA-beer-day-diabetes-bay-scientific-review-finds.html&psig=AOvVaw3hd5f03NOeMM5oVP1XNLsJ&ust=1587379220785000&source=images&cd=vfe&ved=0CAIQjRxqFwoTCIjHu8em9OgCFQAAAAAdAAAAABAI",
            };

            var result = await service.CreateAsync(product.Name, product.Price, product.Description, product.ImageUrl, product.CategoryId);

            Assert.Equal(product.CategoryId, result.CategoryId);
            Assert.Equal(product.Name, result.Name);
            Assert.Equal(product.Description, result.Description);
            Assert.Equal(product.Price, result.Price);
            Assert.Equal(product.ImageUrl, result.ImageUrl);
        }

        [Theory]
        [InlineData(1)]
        public async Task GetProductByIdCorrectly(int id)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            var dbContext = new ApplicationDbContext(options);

            var repository = new EfDeletableEntityRepository<Product>(dbContext);
            var service = new ProductService(repository);

            var product = new Product
            {
                CategoryId = 1,
                Name = "Tiramisu",
                Description = "450g.",
                Price = 5.5m,
                ImageUrl = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.dailymail.co.uk%2Fhealth%2Farticle-7469271%2FA-beer-day-diabetes-bay-scientific-review-finds.html&psig=AOvVaw3hd5f03NOeMM5oVP1XNLsJ&ust=1587379220785000&source=images&cd=vfe&ved=0CAIQjRxqFwoTCIjHu8em9OgCFQAAAAAdAAAAABAI",
            };

            await repository.AddAsync(product);
            await repository.SaveChangesAsync();

            var result = service.GetProductById(id);

            Assert.Equal(product, result);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        public async Task GetProductByIdWrongly(int id)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            var dbContext = new ApplicationDbContext(options);

            var repository = new EfDeletableEntityRepository<Product>(dbContext);
            var service = new ProductService(repository);

            var product = new Product
            {
                CategoryId = 1,
                Name = "Tiramisu",
                Description = "450g.",
                Price = 5.5m,
                ImageUrl = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.dailymail.co.uk%2Fhealth%2Farticle-7469271%2FA-beer-day-diabetes-bay-scientific-review-finds.html&psig=AOvVaw3hd5f03NOeMM5oVP1XNLsJ&ust=1587379220785000&source=images&cd=vfe&ved=0CAIQjRxqFwoTCIjHu8em9OgCFQAAAAAdAAAAABAI",
            };

            await repository.AddAsync(product);
            await repository.SaveChangesAsync();

            var result = service.GetProductById(id);

            Assert.NotEqual(product, result);
        }

        [Theory]
        [InlineData(1)]
        public async Task GetPriceByIdCorrectly(int id)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            var dbContext = new ApplicationDbContext(options);

            var repository = new EfDeletableEntityRepository<Product>(dbContext);
            var service = new ProductService(repository);

            var product = new Product
            {
                CategoryId = 1,
                Name = "Tiramisu",
                Description = "450g.",
                Price = 5.5m,
                ImageUrl = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.dailymail.co.uk%2Fhealth%2Farticle-7469271%2FA-beer-day-diabetes-bay-scientific-review-finds.html&psig=AOvVaw3hd5f03NOeMM5oVP1XNLsJ&ust=1587379220785000&source=images&cd=vfe&ved=0CAIQjRxqFwoTCIjHu8em9OgCFQAAAAAdAAAAABAI",
            };

            await repository.AddAsync(product);
            await repository.SaveChangesAsync();

            var price = service.GetPriceById(id);

            Assert.Equal(product.Price, price);
        }

        [Theory]
        [InlineData(1)]
        public async Task GetPriceByIdWrongly(int id)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            var dbContext = new ApplicationDbContext(options);

            var repository = new EfDeletableEntityRepository<Product>(dbContext);
            var service = new ProductService(repository);

            var product1 = new Product
            {
                CategoryId = 1,
                Name = "Tiramisu",
                Description = "450g.",
                Price = 5.5m,
                ImageUrl = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.dailymail.co.uk%2Fhealth%2Farticle-7469271%2FA-beer-day-diabetes-bay-scientific-review-finds.html&psig=AOvVaw3hd5f03NOeMM5oVP1XNLsJ&ust=1587379220785000&source=images&cd=vfe&ved=0CAIQjRxqFwoTCIjHu8em9OgCFQAAAAAdAAAAABAI",
            };

            var product2 = new Product
            {
                CategoryId = 1,
                Name = "Margheritta",
                Description = "450g.",
                Price = 13.5m,
                ImageUrl = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.dailymail.co.uk%2Fhealth%2Farticle-7469271%2FA-beer-day-diabetes-bay-scientific-review-finds.html&psig=AOvVaw3hd5f03NOeMM5oVP1XNLsJ&ust=1587379220785000&source=images&cd=vfe&ved=0CAIQjRxqFwoTCIjHu8em9OgCFQAAAAAdAAAAABAI",
            };
            await repository.AddAsync(product1);
            await repository.AddAsync(product2);
            await repository.SaveChangesAsync();

            var price = service.GetPriceById(id);

            Assert.NotEqual(product2.Price, price);
        }

        [Theory]
        [InlineData(1)]
        public async Task DeleteProductByIdCorrectly(int id)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            var dbContext = new ApplicationDbContext(options);

            var repository = new EfDeletableEntityRepository<Product>(dbContext);
            var service = new ProductService(repository);

            var product = new Product
            {
                CategoryId = 1,
                Name = "Tiramisu",
                Description = "450g.",
                Price = 5.5m,
                ImageUrl = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.dailymail.co.uk%2Fhealth%2Farticle-7469271%2FA-beer-day-diabetes-bay-scientific-review-finds.html&psig=AOvVaw3hd5f03NOeMM5oVP1XNLsJ&ust=1587379220785000&source=images&cd=vfe&ved=0CAIQjRxqFwoTCIjHu8em9OgCFQAAAAAdAAAAABAI",
            };

            await repository.AddAsync(product);
            await repository.SaveChangesAsync();

            await service.DeleteProductAsync(id);

            Assert.Equal(0, repository.All().Count());
        }

        [Theory]
        [InlineData(1)]
        public async Task UpdateProductById(int id)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            var dbContext = new ApplicationDbContext(options);

            var repository = new EfDeletableEntityRepository<Product>(dbContext);
            var service = new ProductService(repository);

            var product = new Product
            {
                CategoryId = 1,
                Name = "Tiramisu",
                Description = "450g.",
                Price = 5.5m,
                ImageUrl = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.dailymail.co.uk%2Fhealth%2Farticle-7469271%2FA-beer-day-diabetes-bay-scientific-review-finds.html&psig=AOvVaw3hd5f03NOeMM5oVP1XNLsJ&ust=1587379220785000&source=images&cd=vfe&ved=0CAIQjRxqFwoTCIjHu8em9OgCFQAAAAAdAAAAABAI",
            };

            await repository.AddAsync(product);
            await repository.SaveChangesAsync();
            product.Price = 5.3m;
            service.Update(id);

            Assert.Equal(5.3m, product.Price);
        }

        [Fact]
        public async Task EditModel()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            var dbContext = new ApplicationDbContext(options);

            var repository = new EfDeletableEntityRepository<Product>(dbContext);
            var service = new ProductService(repository);

            var product = new Product
            {
                CategoryId = 1,
                Name = "Tiramisu",
                Description = "450g.",
                Price = 5.5m,
                ImageUrl = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.dailymail.co.uk%2Fhealth%2Farticle-7469271%2FA-beer-day-diabetes-bay-scientific-review-finds.html&psig=AOvVaw3hd5f03NOeMM5oVP1XNLsJ&ust=1587379220785000&source=images&cd=vfe&ved=0CAIQjRxqFwoTCIjHu8em9OgCFQAAAAAdAAAAABAI",
            };

            await repository.AddAsync(product);
            await repository.SaveChangesAsync();

            var newProduct = new UpdateProductViewModel
            {
                Description = product.Description,
                Name = product.Name,
                Price = 5.9m,
            };

            await service.EditModel(newProduct, product.Id, null);

            Assert.Equal(5.9m, product.Price);
        }
    }
}
