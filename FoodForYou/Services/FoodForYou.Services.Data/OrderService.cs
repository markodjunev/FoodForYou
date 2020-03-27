﻿namespace FoodForYou.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FoodForYou.Data.Common.Repositories;
    using FoodForYou.Data.Models;
    using FoodForYou.Data.Models.Enums;
    using FoodForYou.Services.Data.Interfaces;
    using FoodForYou.Web.ViewModels.OrderProducts;
    using FoodForYou.Web.ViewModels.Orders;

    public class OrderService : IOrderService
    {
        private readonly IDeletableEntityRepository<Order> ordersRepository;
        private readonly IOrderProductService orderProductService;

        public OrderService(IDeletableEntityRepository<Order> ordersRepository, IOrderProductService orderProductService)
        {
            this.ordersRepository = ordersRepository;
            this.orderProductService = orderProductService;
        }

        public async Task CreateOrderAsync(string userId, string address)
        {
            var order = new Order
            {
                CreatorId = userId,
                Address = address,
            };

            this.orderProductService.SetOrderedProductsToOrder(order);

            await this.ordersRepository.AddAsync(order);
            await this.ordersRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllOrders<T>(string userId)
        {
            throw new NotImplementedException();
        }

        public CompletedOrderViewModel GetLatestOrder(string userId)
        {
            var latestOrder = this.ordersRepository.All()
                .Where(x => x.CreatorId == userId)
                .OrderByDescending(x => x.CreatedOn)
                .Select(o => new CompletedOrderViewModel
                {
                    Address = o.Address,
                    CreatorName = o.Creator.UserName,
                    Price = o.Price,
                    ArrivingTime = o.CreatedOn.AddMinutes(150).ToString("HH:mm", DateTimeFormatInfo.InvariantInfo),
                })
                .FirstOrDefault();

            return latestOrder;
        }

    }
}
