namespace FoodForYou.Web.ViewModels.Reviews
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class CreateReviewInputModel
    {
        [Required]
        [StringLength(int.MaxValue, ErrorMessage = "The {0} must be at least {2} characters", MinimumLength = 10)]
        public string Text { get; set; }
    }
}
