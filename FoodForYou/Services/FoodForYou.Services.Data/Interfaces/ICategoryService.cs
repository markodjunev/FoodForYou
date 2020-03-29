namespace FoodForYou.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface ICategoryService
    {
        Task CreateAsync(string name, string imageUrl);

        IEnumerable<T> GetAll<T>();

        int GetIdByName(string categoryName);
    }
}
