namespace FoodForYou.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FoodForYou.Data;
    using FoodForYou.Data.Common.Repositories;
    using FoodForYou.Data.Models;
    using FoodForYou.Data.Models.Enums;
    using FoodForYou.Data.Repositories;
    using FoodForYou.Services.Data;
    using FoodForYou.Services.Interfaces;
    using FoodForYou.Web.ViewModels.Categories;
    using FoodForYou.Web.ViewModels.OrderProducts;
    using FoodForYou.Web.ViewModels.Products;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class OrdersServiceTest
    {
        private const string UserId1 = "xxx";
        private const string UserId2 = "yyy";

        [Theory]
        [InlineData(UserId1, "ul.Tintyava 15")]
        public async Task CreateOrderSuccesfully(string creatorId, string address)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            var dbContext = new ApplicationDbContext(options);

            var orderProductsRepository = new EfDeletableEntityRepository<OrderProduct>(dbContext);
            var orderProductsService = new OrderProductService(orderProductsRepository);

            var orderProduct = new OrderProduct
            {
                CreatorId = creatorId,
                ProductId = 1,
                Quantity = 1,
                Price = 7m,
                Status = OrderProductStatus.Active,
            };

            await orderProductsRepository.AddAsync(orderProduct);
            await orderProductsRepository.SaveChangesAsync();

            var ordersRepository = new EfDeletableEntityRepository<Order>(dbContext);
            var ordersService = new OrderService(ordersRepository, orderProductsService);

            await ordersService.CreateOrderAsync(creatorId, address);

            Assert.Single(ordersRepository.All().Where(x => x.CreatorId == creatorId));
            Assert.Equal(7m, ordersRepository.All().Where(x => x.CreatorId == creatorId).FirstOrDefault().Price);
        }

        [Theory]
        [InlineData(UserId1)]
        public async Task GetLatestOrder(string userId)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            var dbContext = new ApplicationDbContext(options);

            var orderProductsRepository = new EfDeletableEntityRepository<OrderProduct>(dbContext);
            var orderProductsService = new OrderProductService(orderProductsRepository);

            var orderProduct1 = new OrderProduct
            {
                CreatorId = userId,
                ProductId = 1,
                Quantity = 1,
                Price = 7m,
                Status = OrderProductStatus.Active,
            };

            var orderProduct2 = new OrderProduct
            {
                CreatorId = userId,
                ProductId = 2,
                Quantity = 1,
                Price = 5m,
                Status = OrderProductStatus.Active,
            };

            await orderProductsRepository.AddAsync(orderProduct1);
            await orderProductsRepository.AddAsync(orderProduct2);
            await orderProductsRepository.SaveChangesAsync();

            var ordersRepository = new EfDeletableEntityRepository<Order>(dbContext);
            var ordersService = new OrderService(ordersRepository, orderProductsService);

            await ordersService.CreateOrderAsync(userId, "ul.Tintyava 15");

            var completedOrder = ordersService.GetLatestOrder(userId);

            Assert.Equal(completedOrder.Price, ordersRepository.All().Where(x => x.CreatorId == userId).FirstOrDefault().Price);
        }

        [Theory]
        [InlineData(UserId1)]
        public async Task GetAllOrdersCountByUserId(string userId)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            var dbContext = new ApplicationDbContext(options);

            var ordersRepository = new EfDeletableEntityRepository<Order>(dbContext);
            var ordersService = new OrderService(ordersRepository, null);

            var orders = this.GetOrders();

            for (int i = 0; i < orders.Count(); i++)
            {
                await ordersRepository.AddAsync(orders[i]);
            }

            await ordersRepository.SaveChangesAsync();

            var count = ordersService.GetAllOrdersCountByUserId(userId);

            Assert.Equal(2, count);
        }

        [Fact]
        public async Task GetAllOrdersCount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            var dbContext = new ApplicationDbContext(options);

            var ordersRepository = new EfDeletableEntityRepository<Order>(dbContext);
            var ordersService = new OrderService(ordersRepository, null);

            var orders = this.GetOrders();

            for (int i = 0; i < orders.Count(); i++)
            {
                await ordersRepository.AddAsync(orders[i]);
            }

            await ordersRepository.SaveChangesAsync();

            var count = ordersService.GetAllOrdersCount();

            Assert.Equal(3, count);
        }

        [Fact]
        public async Task GetAllOrdersByUserId()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            var dbContext = new ApplicationDbContext(options);

            var ordersRepository = new EfDeletableEntityRepository<Order>(dbContext);
            var ordersService = new OrderService(ordersRepository, null);

            var orders = this.GetOrders();

            for (int i = 0; i < orders.Count(); i++)
            {
                await ordersRepository.AddAsync(orders[i]);
            }

            await ordersRepository.SaveChangesAsync();

            var allOrders = ordersService.GetAllOrdersByUserId(UserId1);

            Assert.Equal(2, allOrders.Count());
        }

        [Theory]
        [InlineData(UserId1)]
        [InlineData(UserId2)]
        public async Task GetAllLatestOrders(string userId)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            var dbContext = new ApplicationDbContext(options);

            var ordersRepository = new EfDeletableEntityRepository<Order>(dbContext);
            var ordersService = new OrderService(ordersRepository, null);

            var orders = this.GetOrders();

            for (int i = 0; i < orders.Count(); i++)
            {
                await ordersRepository.AddAsync(orders[i]);
            }

            await ordersRepository.SaveChangesAsync();

            var latestOrders = ordersService.GetAllLatestOrders(userId);

            Assert.Equal(3, latestOrders.Count());
        }

        private List<Order> GetOrders()
        {
            var orders = new List<Order>();
            var order1 = new Order
            {
                CreatorId = UserId1,
                Address = "ul. Tintyava 15",
                Products = new List<OrderProduct>
                {
                    new OrderProduct
                    {
                        CreatorId = UserId1,
                        ProductId = 2,
                        Quantity = 1,
                        Price = 5m,
                        Status = OrderProductStatus.Active,
                    },
                    new OrderProduct
                    {
                         CreatorId = UserId1,
                         ProductId = 1,
                         Quantity = 1,
                         Price = 7m,
                         Status = OrderProductStatus.Active,
                    },
                },
                Price = 13m,
            };

            var order2 = new Order
            {
                CreatorId = UserId1,
                Address = "ul. Tintyava 17",
                Products = new List<OrderProduct>
                {
                    new OrderProduct
                    {
                        CreatorId = UserId1,
                        ProductId = 2,
                        Quantity = 1,
                        Price = 25m,
                        Status = OrderProductStatus.Active,
                    },
                    new OrderProduct
                    {
                         CreatorId = UserId1,
                         ProductId = 1,
                         Quantity = 1,
                         Price = 5m,
                         Status = OrderProductStatus.Active,
                    },
                },
                Price = 30m,
            };

            var order3 = new Order
            {
                CreatorId = UserId2,
                Address = "ul. Tintyava 17",
                Products = new List<OrderProduct>
                {
                    new OrderProduct
                    {
                        CreatorId = UserId2,
                        ProductId = 2,
                        Quantity = 1,
                        Price = 5m,
                        Status = OrderProductStatus.Active,
                    },
                    new OrderProduct
                    {
                         CreatorId = UserId2,
                         ProductId = 1,
                         Quantity = 1,
                         Price = 5m,
                         Status = OrderProductStatus.Active,
                    },
                },
                Price = 10m,
            };

            orders.Add(order1);
            orders.Add(order2);
            orders.Add(order3);

            return orders;
        }
    }
}
