namespace FoodForYou.Web.ViewModels.Products
{
    using FoodForYou.Data.Models;
    using FoodForYou.Services.Mapping;

    public class ProductsViewModel : IMapFrom<Product>
    {
        public int Id { get; set; }

        public Category Category { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }
    }
}
