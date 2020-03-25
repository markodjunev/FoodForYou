namespace FoodForYou.Web.ViewModels.OrderProducts
{
    using System.Collections.Generic;

    public class AllProductsInCartViewModel
    {
        public IEnumerable<ProductInCartViewModel> OrderedProducts { get; set; }

        public decimal Sum { get; set; }
    }
}
