namespace FoodForYou.Web.ViewModels.Categories
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Text;

    using FoodForYou.Data.Models;
    using FoodForYou.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    public class CreateCategoryInputModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Image")]
        public IFormFile ImageUrl { get; set; }
    }
}
