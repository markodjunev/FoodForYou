namespace FoodForYou.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using FoodForYou.Data.Common.Repositories;
    using FoodForYou.Data.Models;
    using FoodForYou.Services.Data.Interfaces;

    public class ReviewService : IReviewService
    {
        private readonly IDeletableEntityRepository<Review> reviewsRepository;

        public ReviewService(IDeletableEntityRepository<Review> reviewsRepository)
        {
            this.reviewsRepository = reviewsRepository;
        }

        public async Task CreateAsync(int productId, string creatorId, string text)
        {
            var review = new Review
            {
                ProductId = productId,
                CreatorId = creatorId,
                Text = text,
            };

            await this.reviewsRepository.AddAsync(review);
            await this.reviewsRepository.SaveChangesAsync();
        }
    }
}
