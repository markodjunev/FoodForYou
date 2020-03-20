namespace FoodForYou.Web.ViewModels.Categories
{
    using FoodForYou.Data.Models;
    using FoodForYou.Services.Mapping;

    public class CategoriesViewModel : IMapFrom<Category>
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string Url => $"/food/{this.Name}";
    }
}
