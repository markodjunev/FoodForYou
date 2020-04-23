namespace FoodForYou.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FoodForYou.Data.Models;
    using FoodForYou.Web.ViewModels.OrderProducts;
    using FoodForYou.Web.ViewModels.Orders;

    public interface IOrderProductService
    {
        Task CreateOrderProductAsync(int productId, string creatorId, decimal productPrice, int quantity);

        IEnumerable<T> GetAllByUserId<T>(string userId);

        Task ClearCartAsync(IEnumerable<ClearOrderedProductInCart> orderedProducts);

        void SetOrderedProductsToOrder(Order order);

        int CurrentCountOrderProductsByUserId(string userId);
    }
}
