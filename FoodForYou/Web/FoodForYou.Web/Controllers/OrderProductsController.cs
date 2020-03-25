namespace FoodForYou.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FoodForYou.Data.Models;
    using FoodForYou.Services.Data.Interfaces;
    using FoodForYou.Services.Mapping;
    using FoodForYou.Web.ViewModels.OrderProducts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class OrderProductsController : BaseController
    {
        private readonly IOrderProductService orderProductsService;
        private readonly IProductService productsService;
        private readonly UserManager<ApplicationUser> userManager;

        public OrderProductsController(IOrderProductService orderProductsService, IProductService productsService, UserManager<ApplicationUser> userManager)
        {
            this.orderProductsService = orderProductsService;
            this.productsService = productsService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult AddToCart(int id)
        {
            this.ViewBag.ProductName = this.productsService.GetProductById(id).Name;
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddToCart(int id, CreateOrderProductInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            var productPrice = this.productsService.GetPriceById(id);

            await this.orderProductsService.CreateOrderProductAsync(id, user.Id, productPrice, input.Quantity);
            return this.Redirect("/Categories/All");
        }
    }
}
