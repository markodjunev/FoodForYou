namespace FoodForYou.Web.Areas.Administration.Controllers
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

    public class ProductsController : AdministrationController
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

        public IActionResult Create(string categoryName)
        {
            this.ViewBag.CategoryName = categoryName;
            return this.View();
        }

        [HttpPost]
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

            return this.RedirectToAction("ByCategory", "Products", new { area = string.Empty, categoryName });
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.productsService.DeleteProduct(id);

            return this.RedirectToAction("All", "Categories", new { area = string.Empty });
        }
    }
}
