namespace FoodForYou.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FoodForYou.Common;
    using FoodForYou.Data.Models;
    using FoodForYou.Services.Data.Interfaces;
    using FoodForYou.Services.Mapping;
    using FoodForYou.Web.ViewModels.OrderProducts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
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

        public IActionResult AddToCart(int id)
        {
            this.ViewBag.ProductName = this.productsService.GetProductById(id).Name;
            return this.View();
        }

        [HttpPost]
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

        public async Task<IActionResult> All()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var orderProducts = new AllProductsInCartViewModel
            {
                OrderedProducts = this.orderProductsService.GetAllByUserId<ProductInCartViewModel>(user.Id),
            };

            orderProducts.Sum = orderProducts.OrderedProducts.Sum(x => x.Price);
            return this.View(orderProducts);
        }

        public async Task<IActionResult> Clear()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var orderProducts = new ClearAllOrderedProductsInCart
            {
                OrderedProducts = this.orderProductsService.GetAllByUserId<ClearOrderedProductInCart>(user.Id),
            };

            await this.orderProductsService.ClearCartAsync(orderProducts.OrderedProducts);
            return this.Redirect("/Categories/All");
        }
    }
}
