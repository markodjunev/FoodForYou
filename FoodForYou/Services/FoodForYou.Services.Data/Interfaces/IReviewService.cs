namespace FoodForYou.Services.Data.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using FoodForYou.Data.Models;
    using FoodForYou.Web.ViewModels.Reviews;

    public interface IReviewService
    {
        Task CreateAsync(int productId, string creatorId, string text);

        IEnumerable<ReviewViewModel> GetAllReviewsByProductId(int productId, int? take = null, int skip = 0);

        Task<int> DeleteAsync(int id);

        bool ExistReview(int id);

        int GetAllReviewsCountByProductId(int id);

        Review GetReviewById(int id);
    }
}
