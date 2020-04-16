namespace FoodForYou.Web.ViewModels.Products
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using FoodForYou.Data.Models;
    using FoodForYou.Services.Mapping;

    public class ProductDetailsViewModel : IMapFrom<Product>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public virtual ICollection<ApplicationUserFavouriteProduct> ApplicationUserFavouriteProducts { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}
