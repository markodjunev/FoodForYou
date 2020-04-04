namespace FoodForYou.Web.Areas.Administration.Controllers
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

    public class OrdersController : AdministrationController
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

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> LatestOrders(int page = 1)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var latestOrders = new LatestOrdersViewModel
            {
                AllOrders = this.ordersService.GetAllLatestOrders(user.Id, ItemsPerPage, (page - 1) * ItemsPerPage),
            };

            var count = this.ordersService.GetAllOrdersCount();

            latestOrders.PagesCount = (int)Math.Ceiling((double)count / ItemsPerPage);
            if (latestOrders.PagesCount == 0)
            {
                latestOrders.PagesCount = 1;
            }

            latestOrders.CurrentPage = page;
            latestOrders.OrdersCount = count;

            if (latestOrders.CurrentPage > latestOrders.PagesCount)
            {
                return this.BadRequest();
            }

            return this.View(latestOrders);
        }
    }
}
