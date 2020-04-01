namespace FoodForYou.Services.Data
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

        public IEnumerable<AllOrdersViewModel> GetAllOrders(string userId, int? take = null, int skip = 0)
        {
            var orders = this.ordersRepository.All()
                .Where(x => x.CreatorId == userId)
                .OrderByDescending(x => x.CreatedOn)
                .Select(o => new AllOrdersViewModel
                {
                    Address = o.Address,
                    Price = o.Price,
                    CreatedOn = o.CreatedOn.ToString(@"MM\/dd\/yyyy HH:mm", DateTimeFormatInfo.InvariantInfo),
                    OrderProducts = o.Products.Select(op => new OrderProductsViewModel
                    {
                        ProductName = op.Product.Name,
                        Price = op.Price,
                        Quantity = op.Quantity,
                    })
                    .ToList(),
                })
                .Skip(skip);

            if (take.HasValue)
            {
                orders = orders.Take(take.Value);
            }

            return orders.ToList();
        }

        public int GetAllOrdersCountByUserId(string userId)
        {
            var orders = this.ordersRepository.All().Where(x => x.CreatorId == userId);

            return orders.Count();
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
                    ArrivingTime = o.CreatedOn.AddMinutes(210).ToString("HH:mm", DateTimeFormatInfo.InvariantInfo),
                })
                .FirstOrDefault();

            return latestOrder;
        }
    }
}
