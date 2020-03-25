namespace FoodForYou.Services.Data.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IOrderProductService
    {
        Task CreateOrderProductAsync(int productId, string creatorId, decimal productPrice, int quantity);
    }
}
