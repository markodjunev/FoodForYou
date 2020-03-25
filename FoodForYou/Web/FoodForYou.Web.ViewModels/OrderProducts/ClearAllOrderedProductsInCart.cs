namespace FoodForYou.Web.ViewModels.OrderProducts
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ClearAllOrderedProductsInCart
    {
        public IEnumerable<ClearOrderedProductInCart> OrderedProducts { get; set; }
    }
}
