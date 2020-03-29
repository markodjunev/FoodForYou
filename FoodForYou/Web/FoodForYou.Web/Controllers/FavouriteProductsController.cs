namespace FoodForYou.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FoodForYou.Data.Models;
    using FoodForYou.Services.Data.Interfaces;
    using FoodForYou.Web.ViewModels.FavouriteProducts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class FavouriteProductsController : BaseController
    {
        private readonly IFavouriteProductService favouriteProductService;
        private readonly UserManager<ApplicationUser> userManager;

        public FavouriteProductsController(IFavouriteProductService favouriteProductService, UserManager<ApplicationUser> userManager)
        {
            this.favouriteProductService = favouriteProductService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> All()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var favProducts = new AllFavouriteProductsViewModel
            {
               Products = this.favouriteProductService.GetAllFavouriteProducts(user.Id),
            };

            return this.View(favProducts);
        }

        public async Task<IActionResult> AddProductToUser(int productId)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            await this.favouriteProductService.AddProductToUser(productId, user.Id);

            return this.RedirectToAction("Details", "Products", new { id = productId });
        }

        public async Task<IActionResult> RemoveFavouriteProduct(int productId)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            await this.favouriteProductService.RemoveFavouriteProduct(productId, user.Id);

            return this.RedirectToAction("Details", "Products", new { id = productId });
        }
    }
}
