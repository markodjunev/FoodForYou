namespace FoodForYou.Services.Data.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using FoodForYou.Web.ViewModels.FavouriteProducts;

    public interface IFavouriteProductService
    {
        Task AddProductToUserAsync(int productId, string userId);

        Task RemoveFavouriteProductAsync(int productId, string userId);

        IEnumerable<FavouriteProductViewModel> GetAllFavouriteProducts(string userId);

        bool IsProductAdded(int productId, string userId);
    }
}
