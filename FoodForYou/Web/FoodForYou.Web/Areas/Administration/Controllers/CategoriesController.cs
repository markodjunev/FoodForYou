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
    using FoodForYou.Web.ViewModels.Categories;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class CategoriesController : AdministrationController
    {
        private readonly ICategoryService categoryService;
        private readonly ICloudinaryService cloudinaryService;

        public CategoriesController(ICategoryService categoryService, ICloudinaryService cloudinaryService)
        {
            this.categoryService = categoryService;
            this.cloudinaryService = cloudinaryService;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            string pictureUrl = await this.cloudinaryService.UploadPictureAsync(
                input.ImageUrl,
                input.Name);

            await this.categoryService.CreateAsync(input.Name, pictureUrl);

            return this.RedirectToAction("All", "Categories", new { area = string.Empty });
        }
    }
}
