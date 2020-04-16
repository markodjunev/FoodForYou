namespace FoodForYou.Web.ViewModels.Orders
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class CreateOrderInputModel
    {
        [Required]
        [StringLength(1000, ErrorMessage = "Invalid Address", MinimumLength = 5)]
        public string Address { get; set; }
    }
}
