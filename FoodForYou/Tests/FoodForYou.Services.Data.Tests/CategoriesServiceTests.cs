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
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class CategoriesServiceTests
    {
        [Fact]
        public async Task CreateMethodShouldAddCategoryInDatabase()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var dbContext = new ApplicationDbContext(options);

            var repository = new EfDeletableEntityRepository<Category>(dbContext);
            var service = new CategoryService(repository);

            var category = new Category
            {
                Name = "Pizza",
                ImageUrl = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.dailymail.co.uk%2Fhealth%2Farticle-7469271%2FA-beer-day-diabetes-bay-scientific-review-finds.html&psig=AOvVaw3hd5f03NOeMM5oVP1XNLsJ&ust=1587379220785000&source=images&cd=vfe&ved=0CAIQjRxqFwoTCIjHu8em9OgCFQAAAAAdAAAAABAI",
            };

            await service.CreateAsync(category.Name, category.ImageUrl);

            Assert.Equal(1, repository.All().Count());
        }

        [Theory]
        [InlineData("Pizza")]
        public async Task GetIdByNameCorrectly(string name)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var dbContext = new ApplicationDbContext(options);

            var repository = new EfDeletableEntityRepository<Category>(dbContext);
            var service = new CategoryService(repository);

            var category = new Category
            {
                Name = name,
                ImageUrl = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.dailymail.co.uk%2Fhealth%2Farticle-7469271%2FA-beer-day-diabetes-bay-scientific-review-finds.html&psig=AOvVaw3hd5f03NOeMM5oVP1XNLsJ&ust=1587379220785000&source=images&cd=vfe&ved=0CAIQjRxqFwoTCIjHu8em9OgCFQAAAAAdAAAAABAI",
            };

            await service.CreateAsync(category.Name, category.ImageUrl);

            var id = service.GetIdByName(name);

            Assert.Equal(1, id);
        }

        [Theory]
        [InlineData("Pizza")]
        public async Task ExistCategoryCorrectly(string name)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var dbContext = new ApplicationDbContext(options);

            var repository = new EfDeletableEntityRepository<Category>(dbContext);
            var service = new CategoryService(repository);

            var category = new Category
            {
                Name = name,
                ImageUrl = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.dailymail.co.uk%2Fhealth%2Farticle-7469271%2FA-beer-day-diabetes-bay-scientific-review-finds.html&psig=AOvVaw3hd5f03NOeMM5oVP1XNLsJ&ust=1587379220785000&source=images&cd=vfe&ved=0CAIQjRxqFwoTCIjHu8em9OgCFQAAAAAdAAAAABAI",
            };

            await service.CreateAsync(category.Name, category.ImageUrl);

            var exist = service.Exist(name);

            Assert.True(exist);
        }

        [Theory]
        [InlineData("Pizza")]
        public async Task ExistCategoryFalse(string name)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var dbContext = new ApplicationDbContext(options);

            var repository = new EfDeletableEntityRepository<Category>(dbContext);
            var service = new CategoryService(repository);

            var category = new Category
            {
                Name = "Pasta",
                ImageUrl = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.dailymail.co.uk%2Fhealth%2Farticle-7469271%2FA-beer-day-diabetes-bay-scientific-review-finds.html&psig=AOvVaw3hd5f03NOeMM5oVP1XNLsJ&ust=1587379220785000&source=images&cd=vfe&ved=0CAIQjRxqFwoTCIjHu8em9OgCFQAAAAAdAAAAABAI",
            };

            await service.CreateAsync(category.Name, category.ImageUrl);

            var exist = service.Exist(name);

            Assert.False(exist);
        }
    }
}
