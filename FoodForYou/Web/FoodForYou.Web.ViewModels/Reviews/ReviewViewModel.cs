namespace FoodForYou.Web.ViewModels.Reviews
{
    using System;

    public class ReviewViewModel
    {
        public string Text { get; set; }

        public int ProductId { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatorId { get; set; }
    }
}
