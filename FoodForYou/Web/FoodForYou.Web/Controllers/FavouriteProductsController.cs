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
        private readonly IProductService productsService;
        private readonly UserManager<ApplicationUser> userManager;

        public FavouriteProductsController(IFavouriteProductService favouriteProductService, IProductService productsService, UserManager<ApplicationUser> userManager)
        {
            this.favouriteProductService = favouriteProductService;
            this.productsService = productsService;
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
            if (this.productsService.GetProductById(productId) == null)
            {
                return this.BadRequest();
            }

            var user = await this.userManager.GetUserAsync(this.User);

            if (this.favouriteProductService.IsProductAdded(productId, user.Id))
            {
                return this.RedirectToAction("All", "FavouriteProducts");
            }

            await this.favouriteProductService.AddProductToUserAsync(productId, user.Id);

            return this.RedirectToAction("Details", "Products", new { id = productId });
        }

        public async Task<IActionResult> RemoveFavouriteProduct(int productId)
        {
            if (this.productsService.GetProductById(productId) == null)
            {
                return this.BadRequest();
            }

            var user = await this.userManager.GetUserAsync(this.User);

            if (!this.favouriteProductService.IsProductAdded(productId, user.Id))
            {
                return this.RedirectToAction("All", "FavouriteProducts");
            }

            await this.favouriteProductService.RemoveFavouriteProductAsync(productId, user.Id);

            return this.RedirectToAction("Details", "Products", new { id = productId });
        }
    }
}
