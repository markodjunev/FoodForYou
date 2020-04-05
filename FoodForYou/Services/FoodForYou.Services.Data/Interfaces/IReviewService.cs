namespace FoodForYou.Services.Data.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IReviewService
    {
        Task CreateAsync(int productId, string creatorId, string text);
    }
}
