namespace FoodForYou.Web.ViewModels.Reviews
{
    using System.ComponentModel.DataAnnotations;

    public class CreateReviewInputModel
    {
        [Required]
        public string Text { get; set; }
    }
}
