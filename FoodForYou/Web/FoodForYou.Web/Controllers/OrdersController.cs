namespace FoodForYou.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

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
        private readonly IOrderService ordersService;
        private readonly IOrderProductService orderProductsService;
        private readonly UserManager<ApplicationUser> userManager;

        public OrdersController(IOrderService ordersService, IOrderProductService orderProductsService, UserManager<ApplicationUser> userManager)
        {
            this.ordersService = ordersService;
            this.orderProductsService = orderProductsService;
            this.userManager = userManager;
        }

        public IActionResult ProceedToOrder()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> ProceedToOrder(CreateOrderInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            /*var orderProducts = new AllProductsInCartViewModel
            {
                OrderedProducts = this.orderProductsService.GetAllByUserId<ProductInCartViewModel>(user.Id),
            };

            orderProducts.Sum = orderProducts.OrderedProducts.Sum(x => x.Price);*/

            await this.ordersService.CreateOrderAsync(user.Id, input.Address);
            return this.Redirect("/");
        }
    }
}
