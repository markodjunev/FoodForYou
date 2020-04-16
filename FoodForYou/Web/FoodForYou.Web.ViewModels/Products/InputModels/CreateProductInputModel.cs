namespace FoodForYou.Web.ViewModels.Products
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using FoodForYou.Data.Models;
    using FoodForYou.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    public class CreateProductInputModel : IMapTo<Product>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0.01d, 1000d, ErrorMessage = "The price must be a positive number between 0.01 and 1000")]
        public decimal Price { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Image")]
        public IFormFile ImageUrl { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
