namespace FoodForYou.Web.ViewModels.Reviews
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllReviewsViewModel
    {
        public IEnumerable<ReviewViewModel> Reviews { get; set; }

        public int ProductId { get; set; }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public int ReviewsCount { get; set; }

        public int PreviousPage => this.CurrentPage == 1 ? 1 : this.CurrentPage - 1;

        public int NextPage => this.CurrentPage == this.PagesCount ? this.PagesCount : this.CurrentPage + 1;
    }
}
