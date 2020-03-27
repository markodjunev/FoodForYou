namespace FoodForYou.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using FoodForYou.Data.Common.Models;
    using FoodForYou.Data.Models.Enums;

    public class OrderProduct : BaseDeletableModel<int>
    {
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public string CreatorId { get; set; }

        public virtual ApplicationUser Creator { get; set; }

        public int? OrderId { get; set; }

        public virtual Order Order { get; set; }

        public OrderProductStatus Status { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
