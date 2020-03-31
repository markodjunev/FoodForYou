namespace FoodForYou.Services.Data.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using FoodForYou.Web.ViewModels.FavouriteProducts;

    public interface IFavouriteProductService
    {
        Task AddProductToUser(int productId, string userId);

        Task RemoveFavouriteProduct(int productId, string userId);

        IEnumerable<FavouriteProductViewModel> GetAllFavouriteProducts(string userId);
    }
}
