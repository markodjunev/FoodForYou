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

    [Authorize]
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

        public IActionResult Details(int id)
        {
            var product = this.productsService.GetById<ProductDetailsViewModel>(id);

            // this.ViewBag.ProductName = product.Name;
            return this.View(product);
        }

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
