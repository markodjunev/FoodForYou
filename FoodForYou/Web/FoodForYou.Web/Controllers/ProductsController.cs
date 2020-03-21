namespace FoodForYou.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FoodForYou.Common;
    using FoodForYou.Data.Models;
    using FoodForYou.Services.Data.Interfaces;
    using FoodForYou.Services.Mapping;
    using FoodForYou.Web.ViewModels.Products;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class ProductsController : BaseController
    {
        private readonly IProductService productsService;
        private readonly ICloudinaryService cloudinaryService;

        public ProductsController(IProductService productsService, ICloudinaryService cloudinaryService)
        {
            this.productsService = productsService;
            this.cloudinaryService = cloudinaryService;
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Create(int categoryId)
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Create(int categoryId, CreateProductInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            string imageUrl = await this.cloudinaryService.UploadPictureAsync(
                input.ImageUrl,
                input.Name);

            var product = new Product
            {
                Name = input.Name,
                Price = input.Price,
                Description = input.Description,
                ImageUrl = imageUrl,
                CategoryId = categoryId,
            };

            var result = await this.productsService.CreateAsync(product.Name, product.Price, product.Description, product.ImageUrl, product.CategoryId);

            return this.Redirect($"ffy/{result.Category.Name}");
        }

        [Authorize]
        public IActionResult ByCategory(string categoryName)
        {
            var allProductsViewModel = new AllProductsViewModel
            {
                Products = this.productsService.GetAll<ProductsViewModel>(categoryName),
            };

            return this.View(allProductsViewModel);
        }
    }
}
