﻿namespace FoodForYou.Services.Data.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using FoodForYou.Web.ViewModels.OrderProducts;
    using FoodForYou.Web.ViewModels.Orders;

    public interface IOrderService
    {
        Task CreateOrderAsync(string userId, string address);

        CompletedOrderViewModel GetLatestOrder(string userId);

        IEnumerable<AllOrdersViewModel> GetAllOrders(string userId, int? take = null, int skip = 0);

        int GetAllOrdersCountByUserId(string userId);
    }
}
