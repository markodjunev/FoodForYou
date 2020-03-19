namespace FoodForYou.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface ICategoryService
    {
        Task CreateAsync();

        IEnumerable<T> GetAll<T>();
    }
}
