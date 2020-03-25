namespace FoodForYou.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FoodForYou.Data.Models;

    public interface IProductService
    {
        Task<Product> CreateAsync(string name, decimal price, string description, string imageUrl, int categoryId);

        IEnumerable<T> GetAll<T>(string categoryName);

        T GetById<T>(int id);

        Product GetProductById(int id);

        decimal GetPriceById(int id);
    }
}
