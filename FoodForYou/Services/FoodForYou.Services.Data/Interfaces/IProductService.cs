namespace FoodForYou.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FoodForYou.Data.Models;
    using FoodForYou.Web.ViewModels.Products;

    public interface IProductService
    {
        Task<Product> CreateAsync(string name, decimal price, string description, string imageUrl, int categoryId);

        IEnumerable<T> GetAll<T>(string categoryName);

        T GetById<T>(int id);

        Product GetProductById(int id);

        decimal GetPriceById(int id);

        Task DeleteProduct(int id);

        UpdateProductViewModel Update(int id);

        Task EditModel(UpdateProductViewModel model, int id);
    }
}
