namespace FoodForYou.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using FoodForYou.Data.Common.Repositories;
    using FoodForYou.Data.Models;
    using FoodForYou.Data.Models.Enums;
    using FoodForYou.Services.Data.Interfaces;

    public class OrderProductService : IOrderProductService
    {
        private readonly IDeletableEntityRepository<OrderProduct> orderProductRepository;

        public OrderProductService(IDeletableEntityRepository<OrderProduct> orderProductRepository)
        {
            this.orderProductRepository = orderProductRepository;
        }

        public async Task CreateOrderProductAsync(int productId, string creatorId, decimal productPrice, int quantity)
        {
            var orderProduct = new OrderProduct
            {
                ProductId = productId,
                CreatorId = creatorId,
                Quantity = quantity,
                Price = quantity * productPrice,
                Status = OrderProductStatus.Active,
            };

            await this.orderProductRepository.AddAsync(orderProduct);
            await this.orderProductRepository.SaveChangesAsync();
        }
    }
}
