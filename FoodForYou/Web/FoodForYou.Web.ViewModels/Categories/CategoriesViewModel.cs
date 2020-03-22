namespace FoodForYou.Web.ViewModels.Categories
{
    using FoodForYou.Data.Models;
    using FoodForYou.Services.Mapping;

    public class CategoriesViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }
    }
}
