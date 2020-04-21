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
    using FoodForYou.Data.Models.Enums;
    using FoodForYou.Data.Repositories;
    using FoodForYou.Services.Data;
    using FoodForYou.Services.Interfaces;
    using FoodForYou.Web.ViewModels.Categories;
    using FoodForYou.Web.ViewModels.OrderProducts;
    using FoodForYou.Web.ViewModels.Products;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class FavouriteProductsServiceTest
    {
        private const string UserId = "xxx";

        [Theory]
        [InlineData(1, UserId)]
        [InlineData(2, UserId)]
        [InlineData(3, UserId)]
        public async Task AddProductToUserSucefully(int productId, string userId)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
              .Options;

            var dbContext = new ApplicationDbContext(options);

            var repository = new EfDeletableEntityRepository<ApplicationUserFavouriteProduct>(dbContext);
            var service = new FavouriteProductService(repository);

            await service.AddProductToUserAsync(productId, userId);

            Assert.Equal(1, repository.All().Count());
        }

        [Fact]
        public async Task RemoveFavouriteProductSuccefully()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
              .Options;

            var dbContext = new ApplicationDbContext(options);

            var repository = new EfDeletableEntityRepository<ApplicationUserFavouriteProduct>(dbContext);
            var service = new FavouriteProductService(repository);

            var favProducts = this.GetFavProducts(UserId);

            for (int i = 0; i < favProducts.Count(); i++)
            {
                await repository.AddAsync(favProducts[i]);
            }

            await repository.SaveChangesAsync();

            Assert.Equal(4, repository.All().Count()); // before removing

            await service.RemoveFavouriteProductAsync(1, UserId);

            Assert.Equal(3, repository.All().Count()); // after removing
        }

        [Fact]
        public async Task IsProductAlreadyAddedSuccesfully()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
              .Options;

            var dbContext = new ApplicationDbContext(options);

            var repository = new EfDeletableEntityRepository<ApplicationUserFavouriteProduct>(dbContext);
            var service = new FavouriteProductService(repository);

            var favProducts = this.GetFavProducts(UserId);

            for (int i = 0; i < favProducts.Count(); i++)
            {
                await repository.AddAsync(favProducts[i]);
            }

            await repository.SaveChangesAsync();

            var isAdded = service.IsProductAdded(1, UserId);

            Assert.True(isAdded);
        }

        [Fact]
        public async Task IsProductAlreadyAddedWrongly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
              .Options;

            var dbContext = new ApplicationDbContext(options);

            var repository = new EfDeletableEntityRepository<ApplicationUserFavouriteProduct>(dbContext);
            var service = new FavouriteProductService(repository);

            var favProducts = this.GetFavProducts(UserId);

            for (int i = 0; i < favProducts.Count(); i++)
            {
                await repository.AddAsync(favProducts[i]);
            }

            await repository.SaveChangesAsync();

            var isAdded = service.IsProductAdded(10, UserId);

            Assert.False(isAdded);
        }

        [Theory]
        [InlineData(UserId)]
        public async Task GetAllFavProductsByUserId(string userId)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
              .Options;

            var dbContext = new ApplicationDbContext(options);

            var repository = new EfDeletableEntityRepository<ApplicationUserFavouriteProduct>(dbContext);
            var service = new FavouriteProductService(repository);

            var productRepository = new EfDeletableEntityRepository<Product>(dbContext);

            var product1 = new Product
            {
                CategoryId = 1,
                Name = "Caffe",
                ImageUrl = null,
                Price = 5.5m,
            };

            var product2 = new Product
            {
                CategoryId = 2,
                Name = "Bolognese",
                ImageUrl = null,
                Price = 15.5m,
            };

            var product3 = new Product
            {
                CategoryId = 3,
                Name = "Margheritta",
                ImageUrl = null,
                Price = 8.5m,
            };

            await productRepository.AddAsync(product1);
            await productRepository.AddAsync(product2);
            await productRepository.AddAsync(product3);

            await productRepository.SaveChangesAsync();

            var favProducts = this.GetFavProducts(userId);

            for (int i = 0; i < favProducts.Count(); i++)
            {
                await repository.AddAsync(favProducts[i]);
            }

            await repository.SaveChangesAsync();

            var favProductsByUserId = service.GetAllFavouriteProducts(userId);

            Assert.Equal(3, favProductsByUserId.Count());
        }

        private List<ApplicationUserFavouriteProduct> GetFavProducts(string userId)
        {
            var favProducts = new List<ApplicationUserFavouriteProduct>()
            {
                new ApplicationUserFavouriteProduct
                {
                    ProductId = 1,
                    ApplicationUserId = userId,
                },
                new ApplicationUserFavouriteProduct
                {
                    ProductId = 2,
                    ApplicationUserId = userId,
                },
                new ApplicationUserFavouriteProduct
                {
                    ProductId = 3,
                    ApplicationUserId = userId,
                },
                new ApplicationUserFavouriteProduct
                {
                    ProductId = 1,
                    ApplicationUserId = "yyy",
                },
            };

            return favProducts;
        }
    }
}
