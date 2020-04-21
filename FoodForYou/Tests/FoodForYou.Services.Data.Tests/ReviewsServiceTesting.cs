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

    public class ReviewsServiceTesting
    {
        private const string UserId = "xxx";

        [Fact]
        public async Task CreateReviewSuccessfully()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
              .Options;

            var dbContext = new ApplicationDbContext(options);

            var repository = new EfDeletableEntityRepository<Review>(dbContext);
            var service = new ReviewService(repository);

            await service.CreateAsync(1, UserId, "Fantastic");
            await service.CreateAsync(2, UserId, "Not bad");

            Assert.Equal(2, repository.All().Count());
        }

        [Fact]
        public async Task DeleteReviewSuccessfully()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
             .Options;

            var dbContext = new ApplicationDbContext(options);

            var repository = new EfDeletableEntityRepository<Review>(dbContext);
            var service = new ReviewService(repository);

            var reviews = this.GetReviews();

            for (int i = 0; i < reviews.Count(); i++)
            {
                await repository.AddAsync(reviews[i]);
            }

            await repository.SaveChangesAsync();

            await service.DeleteAsync(1);

            Assert.Equal(3, repository.All().Count());
        }

        [Fact]
        public async Task ExistReview()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
              .Options;

            var dbContext = new ApplicationDbContext(options);

            var repository = new EfDeletableEntityRepository<Review>(dbContext);
            var service = new ReviewService(repository);

            var reviews = this.GetReviews();

            for (int i = 0; i < reviews.Count(); i++)
            {
                await repository.AddAsync(reviews[i]);
            }

            await repository.SaveChangesAsync();

            var exist = service.ExistReview(1);

            Assert.True(exist);

            exist = service.ExistReview(10);

            Assert.False(exist);
        }

        [Fact]
        public async Task GetAllReviewsByProductId()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
             .Options;

            var dbContext = new ApplicationDbContext(options);

            var repository = new EfDeletableEntityRepository<Review>(dbContext);
            var service = new ReviewService(repository);

            var reviews = this.GetReviews();

            for (int i = 0; i < reviews.Count(); i++)
            {
                await repository.AddAsync(reviews[i]);
            }

            await repository.SaveChangesAsync();

            var rev = service.GetAllReviewsByProductId(1);

            Assert.Equal(2, rev.Count());
        }

        [Fact]
        public async Task GetAllReviewsCountByProductId()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
             .Options;

            var dbContext = new ApplicationDbContext(options);

            var repository = new EfDeletableEntityRepository<Review>(dbContext);
            var service = new ReviewService(repository);

            var reviews = this.GetReviews();

            for (int i = 0; i < reviews.Count(); i++)
            {
                await repository.AddAsync(reviews[i]);
            }

            await repository.SaveChangesAsync();

            var count1 = service.GetAllReviewsCountByProductId(1);
            var count2 = service.GetAllReviewsCountByProductId(2);
            var count3 = service.GetAllReviewsCountByProductId(3);

            Assert.Equal(2, count1);
            Assert.Equal(1, count2);
            Assert.Equal(1, count3);
        }

        private List<Review> GetReviews()
        {
            var reviews = new List<Review>()
            {
                new Review
                {
                    ProductId = 1,
                    CreatorId = UserId,
                    Text = "Fantastic",
                },
                new Review
                {
                    ProductId = 2,
                    CreatorId = UserId,
                    Text = "Good",
                },
                new Review
                {
                    ProductId = 1,
                    CreatorId = "yyy",
                    Text = "Not bad",
                },
                new Review
                {
                    ProductId = 3,
                    CreatorId = UserId,
                    Text = "Best chef ever",
                },
            };

            return reviews;
        }
    }
}
