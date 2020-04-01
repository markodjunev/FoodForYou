namespace FoodForYou.Web.ViewModels.Orders
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class LatestOrdersViewModel
    {
        public IEnumerable<AllLatestOrdersViewModel> AllOrders { get; set; }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }
    }
}
