namespace FoodForYou.Web.ViewModels.Reviews
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllReviewsViewModel
    {
        public IEnumerable<ReviewViewModel> Reviews { get; set; }

        public int ProductId { get; set; }
    }
}
