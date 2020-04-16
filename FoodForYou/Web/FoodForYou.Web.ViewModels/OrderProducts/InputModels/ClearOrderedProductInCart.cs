namespace FoodForYou.Web.ViewModels.OrderProducts
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using FoodForYou.Data.Models;
    using FoodForYou.Data.Models.Enums;
    using FoodForYou.Services.Mapping;

    public class ClearOrderedProductInCart : IMapFrom<OrderProduct>, IMapTo<OrderProduct>
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public string ProductName { get; set; }

        public string CreatorId { get; set; }

        public virtual ApplicationUser Creator { get; set; }

        public OrderProductStatus Status { get; set; }

        public int Quantity { get; set; }

        public bool IsDeleted { get; set; }

        public decimal Price { get; set; }
    }
}
