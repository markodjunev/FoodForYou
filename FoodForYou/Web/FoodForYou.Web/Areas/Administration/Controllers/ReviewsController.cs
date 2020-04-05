namespace FoodForYou.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FoodForYou.Services.Data.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    public class ReviewsController : AdministrationController
    {
        private readonly IReviewService reviewsService;

        public ReviewsController(IReviewService reviewsService)
        {
            this.reviewsService = reviewsService;
        }

        public async Task<IActionResult> Delete(int id)
        {
            var productId = await this.reviewsService.DeleteAsync(id);

            return this.RedirectToAction("All", "Reviews", new { area = string.Empty, id = productId });
        }
    }
}
