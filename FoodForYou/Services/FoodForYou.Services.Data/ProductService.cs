namespace FoodForYou.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FoodForYou.Data.Common.Repositories;
    using FoodForYou.Data.Models;
    using FoodForYou.Services.Data.Interfaces;
    using FoodForYou.Services.Mapping;
    using FoodForYou.Web.ViewModels.Products;

    public class ProductService : IProductService
    {
        private readonly IDeletableEntityRepository<Product> productsRepository;

        public ProductService(IDeletableEntityRepository<Product> productsRepository)
        {
            this.productsRepository = productsRepository;
        }

        public async Task<Product> CreateAsync(string name, decimal price, string description, string imageUrl, int categoryId)
        {
            var product = new Product
            {
                Name = name,
                Price = price,
                Description = description,
                ImageUrl = imageUrl,
                CategoryId = categoryId,
            };

            await this.productsRepository.AddAsync(product);
            await this.productsRepository.SaveChangesAsync();

            return product;
        }

        public async Task DeleteProduct(int id)
        {
            var product = this.GetProductById(id);

            product.IsDeleted = true;
            this.productsRepository.Update(product);
            await this.productsRepository.SaveChangesAsync();
        }

        public async Task EditModel(UpdateProductViewModel model, int id, string imageUrl)
        {
            var product = this.productsRepository.All().FirstOrDefault(x => x.Id == id);

            product.Name = model.Name;
            product.Price = model.Price;
            product.Description = model.Description;

            if (imageUrl != null)
            {
                product.ImageUrl = imageUrl;
            }

            this.productsRepository.Update(product);
            await this.productsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(string categoryName)
        {
            IQueryable products = this.productsRepository.All()
                .Where(p => p.Category.Name == categoryName).OrderBy(p => p.Name);

            return products.To<T>().ToList();
        }

        public T GetById<T>(int id)
        {
            var product = this.productsRepository.All().Where(p => p.Id == id).To<T>().FirstOrDefault();

            return product;
        }

        public decimal GetPriceById(int id)
        {
            var product = this.productsRepository.All().Where(p => p.Id == id).FirstOrDefault();
            return product.Price;
        }

        public Product GetProductById(int id)
        {
            var product = this.productsRepository.All().Where(p => p.Id == id).FirstOrDefault();

            return product;
        }

        public UpdateProductViewModel Update(int id)
        {
            var product = this.productsRepository.All().FirstOrDefault(x => x.Id == id);

            var model = new UpdateProductViewModel
            {
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
            };

            return model;
        }
    }
}
