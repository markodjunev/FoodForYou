namespace FoodForYou.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FoodForYou.Common;
    using FoodForYou.Data.Models;
    using FoodForYou.Services.Data.Interfaces;
    using FoodForYou.Services.Interfaces;
    using FoodForYou.Services.Mapping;
    using FoodForYou.Web.ViewModels.Products;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class ProductsController : BaseController
    {
        private readonly IProductService productsService;
        private readonly ICloudinaryService cloudinaryService;
        private readonly ICategoryService categoriesService;

        public ProductsController(IProductService productsService, ICloudinaryService cloudinaryService, ICategoryService categoriesService)
        {
            this.productsService = productsService;
            this.cloudinaryService = cloudinaryService;
            this.categoriesService = categoriesService;
        }

        [Authorize]
        public IActionResult Details(int id)
        {
            var product = this.productsService.GetById<ProductDetailsViewModel>(id);

            return this.View(product);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Create(string categoryName)
        {
            this.ViewBag.CategoryName = categoryName;
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Create(string categoryName, CreateProductInputModel input)
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
                CategoryId = this.categoriesService.GetIdByName(categoryName),
            };

            var result = await this.productsService.CreateAsync(product.Name, product.Price, product.Description, product.ImageUrl, product.CategoryId);

            return this.RedirectToAction("ByCategory", new { categoryName });
        }

        [Authorize]
        public IActionResult ByCategory(string categoryName)
        {
            this.ViewBag.CategoryName = categoryName;

            var allProductsViewModel = new AllProductsViewModel
            {
                Products = this.productsService.GetAll<ProductsViewModel>(categoryName),
            };

            return this.View(allProductsViewModel);
        }
    }
}
