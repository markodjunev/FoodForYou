namespace FoodForYou.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using FoodForYou.Data.Common.Repositories;
    using FoodForYou.Data.Models;
    using FoodForYou.Data.Models.Enums;
    using FoodForYou.Services.Data.Interfaces;
    using FoodForYou.Services.Mapping;
    using FoodForYou.Web.ViewModels.OrderProducts;

    public class OrderProductService : IOrderProductService
    {
        private readonly IDeletableEntityRepository<OrderProduct> orderProductRepository;

        public OrderProductService(IDeletableEntityRepository<OrderProduct> orderProductRepository)
        {
            this.orderProductRepository = orderProductRepository;
        }

        public async Task ClearCart(IEnumerable<ClearOrderedProductInCart> orderedProducts)
        {
            foreach (var orderProduct in orderedProducts)
            {
                var product = AutoMapperConfig.MapperInstance.Map<OrderProduct>(orderProduct);
                product.IsDeleted = true;
                this.orderProductRepository.Update(product);
                await this.orderProductRepository.SaveChangesAsync();
            }
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

        public IEnumerable<T> GetAllByUserId<T>(string userId)
        {
            IQueryable orderProducts = this.orderProductRepository.All()
                .Where(x => x.CreatorId == userId && x.Status == OrderProductStatus.Active);

            return orderProducts.To<T>().ToList();
        }
    }
}
