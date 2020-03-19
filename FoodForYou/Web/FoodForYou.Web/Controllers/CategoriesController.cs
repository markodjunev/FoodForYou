namespace FoodForYou.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FoodForYou.Services.Interfaces;
    using FoodForYou.Web.ViewModels.Categories;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class CategoriesController : BaseController
    {
        private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public IActionResult All()
        {
            var categoriesViewModel = new AllCategoriesViewModel
            {
                Categories
                           = this.categoryService.GetAll<CategoriesViewModel>(),
            };

            return this.View(categoriesViewModel);
        }
    }
}
