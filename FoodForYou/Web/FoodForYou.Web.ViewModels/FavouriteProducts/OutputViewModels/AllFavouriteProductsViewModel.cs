namespace FoodForYou.Web.ViewModels.FavouriteProducts
{
    using System.Collections.Generic;

    public class AllFavouriteProductsViewModel
    {
        public IEnumerable<FavouriteProductViewModel> Products { get; set; }
    }
}
