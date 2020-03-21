namespace FoodForYou.Web.ViewModels.Products
{
    using System.Collections.Generic;

    public class AllProductsViewModel
    {
        public IEnumerable<ProductsViewModel> Products { get; set; }
    }
}
