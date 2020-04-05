namespace FoodForYou.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FoodForYou.Data.Common.Repositories;
    using FoodForYou.Data.Models;
    using FoodForYou.Services.Interfaces;
    using FoodForYou.Services.Mapping;

    public class CategoryService : ICategoryService
    {
        private readonly IDeletableEntityRepository<Category> categoriesRepository;

        public CategoryService(IDeletableEntityRepository<Category> categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public async Task CreateAsync(string name, string imageUrl)
        {
            var category = new Category
            {
                Name = name,
                ImageUrl = imageUrl,
            };

            await this.categoriesRepository.AddAsync(category);
            await this.categoriesRepository.SaveChangesAsync();
        }

        public bool Exist(string categoryName)
        {
            var category = this.categoriesRepository.All().FirstOrDefault(x => x.Name == categoryName);

            if (category == null)
            {
                return false;
            }

            return true;
        }

        public IEnumerable<T> GetAll<T>()
        {
            IQueryable<Category> categories = this.categoriesRepository.All();

            return categories.To<T>().ToList();
        }

        public int GetIdByName(string categoryName)
        {
            var category = this.categoriesRepository.All().FirstOrDefault(c => c.Name == categoryName);

            return category.Id;
        }
    }
}
