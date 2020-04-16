namespace FoodForYou.Web.ViewModels.Orders
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllOrdersViewModel
    {
        public IEnumerable<OrderProductsViewModel> OrderProducts { get; set; }

        public string Address { get; set; }

        public decimal Price { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
