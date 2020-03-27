namespace FoodForYou.Web.ViewModels.Orders
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CompletedOrderViewModel
    {
        public string Address { get; set; }

        public string CreatorName { get; set; }

        public decimal Price { get; set; }

        public string ArrivingTime { get; set; }
    }
}
