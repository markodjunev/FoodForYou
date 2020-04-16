namespace FoodForYou.Web.ViewModels.Orders
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class OrdersViewModel
    {
        public IEnumerable<AllOrdersViewModel> AllOrders { get; set; }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public int OrdersCount { get; set; }

        public int PreviousPage => this.CurrentPage == 1 ? 1 : this.CurrentPage - 1;

        public int NextPage => this.CurrentPage == this.PagesCount ? this.PagesCount : this.CurrentPage + 1;
    }
}
