﻿namespace FoodForYou.Services.Data
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

        public bool ExistReview(int id)
        {
            var review = this.reviewsRepository.All()
                .FirstOrDefault(r => r.Id == id);

            if (review == null)
            {
                return false;
            }

            return true;
        }

        public IEnumerable<ReviewViewModel> GetAllReviewsByProductId(int productId, int? take = null, int skip = 0)
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
                }).Skip(skip);

            if (take.HasValue)
            {
                reviews = reviews.Take(take.Value);
            }

            return reviews.ToList();
        }

        public int GetAllReviewsCountByProductId(int id)
        {
            var reviews = this.reviewsRepository.All()
                .Where(r => r.ProductId == id).ToList();

            return reviews.Count();
        }

        public Review GetReviewById(int id)
        {
            var review = this.reviewsRepository.All().FirstOrDefault(x => x.Id == id);

            return review;
        }
    }
}
