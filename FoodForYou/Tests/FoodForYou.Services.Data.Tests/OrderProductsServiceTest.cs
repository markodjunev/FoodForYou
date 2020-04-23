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

    public class OrderProductsServiceTest
    {
        [Fact]
        public async Task CreateOrderProduct()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            var dbContext = new ApplicationDbContext(options);

            var repository = new EfDeletableEntityRepository<OrderProduct>(dbContext);
            var service = new OrderProductService(repository);

            var product = new Product
            {
                Id = 1,
                Name = "Caffe",
                Price = 7m,
            };

            await service.CreateOrderProductAsync(product.Id, "xxx", 7m, 2);

            Assert.Equal(1, repository.All().Count());
            Assert.Equal(14, repository.All().FirstOrDefault().Price); // check price
        }

        [Fact]
        public async Task SetOrderProductsToOrder()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
              .Options;

            var dbContext = new ApplicationDbContext(options);

            var repository = new EfDeletableEntityRepository<OrderProduct>(dbContext);
            var service = new OrderProductService(repository);

            var orderProduct1 = new OrderProduct
            {
                ProductId = 1,
                CreatorId = "xxx",
                Quantity = 1,
                Price = 10,
                Status = OrderProductStatus.Active,
            };

            var orderProduct2 = new OrderProduct
            {
                ProductId = 2,
                CreatorId = "xxx",
                Quantity = 1,
                Price = 5,
                Status = OrderProductStatus.Active,
            };

            await repository.AddAsync(orderProduct1);
            await repository.AddAsync(orderProduct2);
            await repository.SaveChangesAsync();

            var orderRepository = new EfDeletableEntityRepository<Order>(dbContext);
            var orderService = new OrderService(orderRepository, null);

            var order = new Order
            {
                Address = "ul. Tintyava 15",
                CreatorId = "xxx",
                Price = 30m,
            };

            await orderRepository.AddAsync(order);
            await orderRepository.SaveChangesAsync();

            service.SetOrderedProductsToOrder(order);

            Assert.Equal(2, order.Products.Count()); // order contains 2 products
        }

        [Theory]
        [InlineData("xxx")]
        public async Task CurrentCountOrderProductsByUserId(string userId)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
              .Options;

            var dbContext = new ApplicationDbContext(options);

            var repository = new EfDeletableEntityRepository<OrderProduct>(dbContext);
            var service = new OrderProductService(repository);

            var orderProduct1 = new OrderProduct
            {
                ProductId = 1,
                CreatorId = userId,
                Quantity = 1,
                Price = 10,
                Status = OrderProductStatus.Active,
            };

            var orderProduct2 = new OrderProduct
            {
                ProductId = 2,
                CreatorId = userId,
                Quantity = 1,
                Price = 5,
                Status = OrderProductStatus.Active,
            };

            var orderProduct3 = new OrderProduct
            {
                ProductId = 3,
                CreatorId = userId,
                Quantity = 1,
                Price = 5,
                Status = OrderProductStatus.Completed,
            };
            await repository.AddAsync(orderProduct1);
            await repository.AddAsync(orderProduct2);
            await repository.AddAsync(orderProduct3);
            await repository.SaveChangesAsync();

            var count = service.CurrentCountOrderProductsByUserId(userId);

            Assert.Equal(2, count);
        }
    }
}
