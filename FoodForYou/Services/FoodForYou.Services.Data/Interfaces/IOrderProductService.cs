namespace FoodForYou.Services.Data.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using FoodForYou.Web.ViewModels.OrderProducts;

    public interface IOrderProductService
    {
        Task CreateOrderProductAsync(int productId, string creatorId, decimal productPrice, int quantity);

        IEnumerable<T> GetAllByUserId<T>(string userId);

        Task ClearCart(IEnumerable<ClearOrderedProductInCart> orderedProducts);
    }
}
