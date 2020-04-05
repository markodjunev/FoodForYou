namespace FoodForYou.Services.Data.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using FoodForYou.Web.ViewModels.Reviews;

    public interface IReviewService
    {
        Task CreateAsync(int productId, string creatorId, string text);

        IEnumerable<ReviewViewModel> GetAllReviewsByProductId(int productId);

        Task<int> DeleteAsync(int id);
    }
}
