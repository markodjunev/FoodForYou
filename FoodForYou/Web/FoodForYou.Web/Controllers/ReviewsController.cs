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
        private const int ItemsPerPage = 5;

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
            if (this.productsService.GetProductById(id) == null)
            {
                return this.BadRequest();
            }

            this.ViewBag.ProductName = this.productsService.GetProductById(id).Name;
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(int id, CreateReviewInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            await this.reviewsService.CreateAsync(id, user.Id, input.Text);

            return this.RedirectToAction("All", "Reviews", new { id = id });
        }

        public IActionResult All(int id, int page = 1)
        {
            if (this.productsService.GetProductById(id) == null)
            {
                return this.BadRequest();
            }

            this.ViewBag.ProductName = this.productsService.GetProductById(id).Name;
            var viewModel = new AllReviewsViewModel
            {
                ProductId = id,
                Reviews = this.reviewsService.GetAllReviewsByProductId(id, ItemsPerPage, (page - 1) * ItemsPerPage),
            };

            var count = this.reviewsService.GetAllReviewsCountByProductId(id);

            viewModel.PagesCount = (int)Math.Ceiling((double)count / ItemsPerPage);
            if (viewModel.PagesCount == 0)
            {
                viewModel.PagesCount = 1;
            }

            viewModel.CurrentPage = page;
            viewModel.ReviewsCount = count;

            return this.View(viewModel);
        }

        public async Task<IActionResult> Remove(int id)
        {
            if (!this.reviewsService.ExistReview(id))
            {
                return this.BadRequest();
            }

            var productId = await this.reviewsService.DeleteAsync(id);

            return this.RedirectToAction("All", "Reviews", new { id = productId });
        }
    }
}
