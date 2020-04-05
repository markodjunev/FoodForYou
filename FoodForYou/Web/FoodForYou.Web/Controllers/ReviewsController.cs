namespace FoodForYou.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FoodForYou.Data.Models;
    using FoodForYou.Services.Data.Interfaces;
    using FoodForYou.Web.ViewModels.Reviews;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class ReviewsController : BaseController
    {
        private readonly IReviewService reviewsService;
        private readonly IProductService productsService;
        private readonly UserManager<ApplicationUser> userManager;

        public ReviewsController(IReviewService reviewsService, IProductService productsService, UserManager<ApplicationUser> userManager)
        {
            this.reviewsService = reviewsService;
            this.productsService = productsService;
            this.userManager = userManager;
        }

        public IActionResult Add(int id)
        {
            this.ViewBag.ProductName = this.productsService.GetProductById(id).Name;
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(int id, CreateReviewInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            await this.reviewsService.CreateAsync(id, user.Id, input.Text);

            return this.RedirectToAction("All", "Reviews", new { id = id });
        }

        public IActionResult All(int id)
        {
            this.ViewBag.ProductName = this.productsService.GetProductById(id).Name;
            var viewModel = new AllReviewsViewModel
            {
                ProductId = id,
                Reviews = this.reviewsService.GetAllReviewsByProductId(id),
            };
            return this.View(viewModel);
        }
    }
}
