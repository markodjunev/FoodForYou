namespace FoodForYou.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FoodForYou.Data.Common.Repositories;
    using FoodForYou.Data.Models;
    using FoodForYou.Services.Data.Interfaces;
    using FoodForYou.Web.ViewModels.Reviews;

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

        public async Task<int> DeleteAsync(int id)
        {
            var review = this.reviewsRepository.All()
                .Where(r => r.Id == id).FirstOrDefault();

            review.IsDeleted = true;
            this.reviewsRepository.Update(review);
            await this.reviewsRepository.SaveChangesAsync();

            return review.ProductId;
        }

        public IEnumerable<ReviewViewModel> GetAllReviewsByProductId(int productId)
        {
            var reviews = this.reviewsRepository.All()
                .Where(x => x.ProductId == productId)
                .OrderByDescending(x => x.CreatedOn)
                .Select(r => new ReviewViewModel
                {
                    Id = r.Id,
                    Text = r.Text,
                    CreatedOn = r.CreatedOn,
                    CreatorId = r.CreatorId,
                    CreatorUserName = r.Creator.UserName,
                    ProductId = r.ProductId,
                }).ToList();

            return reviews;
        }
    }
}
