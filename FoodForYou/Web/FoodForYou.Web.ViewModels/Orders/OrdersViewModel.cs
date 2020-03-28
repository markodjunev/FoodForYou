namespace FoodForYou.Web.ViewModels.Orders
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class OrdersViewModel
    {
        public IEnumerable<AllOrdersViewModel> AllOrders { get; set; }
    }
}
