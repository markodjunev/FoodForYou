namespace FoodForYou.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FoodForYou.Common;
    using FoodForYou.Data.Models;
    using FoodForYou.Services.Data.Interfaces;
    using FoodForYou.Web.ViewModels.OrderProducts;
    using FoodForYou.Web.ViewModels.Orders;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class OrdersController : BaseController
    {
        private const int ItemsPerPage = 5;

        private readonly IOrderService ordersService;
        private readonly IOrderProductService orderProductsService;
        private readonly UserManager<ApplicationUser> userManager;

        public OrdersController(IOrderService ordersService, IOrderProductService orderProductsService, UserManager<ApplicationUser> userManager)
        {
            this.ordersService = ordersService;
            this.orderProductsService = orderProductsService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> All(int page = 1)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var allOrders = new OrdersViewModel
            {
                AllOrders = this.ordersService.GetAllOrdersByUserId(user.Id, ItemsPerPage, (page - 1) * ItemsPerPage),
            };

            var count = this.ordersService.GetAllOrdersCountByUserId(user.Id);

            allOrders.PagesCount = (int)Math.Ceiling((double)count / ItemsPerPage);
            if (allOrders.PagesCount == 0)
            {
                allOrders.PagesCount = 1;
            }

            allOrders.CurrentPage = page;
            allOrders.OrdersCount = count;

            if (allOrders.CurrentPage > allOrders.PagesCount)
            {
                return this.BadRequest();
            }

            return this.View(allOrders);
        }

        public async Task<IActionResult> CompletedOrder()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var latestOrder = this.ordersService.GetLatestOrder(user.Id);
            return this.View(latestOrder);
        }

        public IActionResult ProceedToOrder()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> ProceedToOrder(CreateOrderInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            await this.ordersService.CreateOrderAsync(user.Id, input.Address);
            return this.Redirect("/Orders/CompletedOrder");
        }
    }
}
