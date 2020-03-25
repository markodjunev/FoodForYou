namespace FoodForYou.Web.ViewModels.OrderProducts
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CreateOrderProductInputModel
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The quantity must be a positive number!")]
        public int Quantity { get; set; }
    }
}
