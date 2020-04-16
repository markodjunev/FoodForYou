namespace FoodForYou.Web.ViewModels.Orders
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class LatestOrderProductsViewModel
    {
        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
