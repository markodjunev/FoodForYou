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
    using FoodForYou.Web.ViewModels.FavouriteProducts;

    public class FavouriteProductService : IFavouriteProductService
    {
        private readonly IDeletableEntityRepository<ApplicationUserFavouriteProduct> favoruiteProductsRepository;

        public FavouriteProductService(IDeletableEntityRepository<ApplicationUserFavouriteProduct> favoruiteProductsRepository)
        {
            this.favoruiteProductsRepository = favoruiteProductsRepository;
        }

        public async Task AddProductToUserAsync(int productId, string userId)
        {
            var favProduct = new ApplicationUserFavouriteProduct
            {
                ProductId = productId,
                ApplicationUserId = userId,
            };

            await this.favoruiteProductsRepository.AddAsync(favProduct);
            await this.favoruiteProductsRepository.SaveChangesAsync();
        }

        public IEnumerable<FavouriteProductViewModel> GetAllFavouriteProducts(string userId)
        {
            var favProducts = this.favoruiteProductsRepository.All()
                .Where(x => x.ApplicationUserId == userId)
                .OrderByDescending(x => x.CreatedOn)
                .Select(p => new FavouriteProductViewModel()
                {
                    ProductId = p.ProductId,
                    Name = p.Product.Name,
                    Description = p.Product.Description,
                    Price = p.Product.Price,
                    ImageUrl = p.Product.ImageUrl,
                }).ToList();

            return favProducts;
        }

        public bool IsProductAdded(int productId, string userId)
        {
            var isAdded = this.favoruiteProductsRepository.All().Any(x => x.ProductId == productId && x.ApplicationUserId == userId);
            return isAdded;
        }

        public async Task RemoveFavouriteProductAsync(int productId, string userId)
        {
            var favProduct = this.favoruiteProductsRepository.All()
                .Where(x => x.ProductId == productId && x.ApplicationUserId == userId).FirstOrDefault();

            favProduct.IsDeleted = true;
            this.favoruiteProductsRepository.Update(favProduct);
            await this.favoruiteProductsRepository.SaveChangesAsync();
        }
    }
}
