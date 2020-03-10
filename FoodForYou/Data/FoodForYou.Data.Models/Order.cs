namespace FoodForYou.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using FoodForYou.Data.Common.Models;
    using FoodForYou.Data.Models.Enums;

    public class Order : BaseDeletableModel<int>
    {
        public Order()
        {
            this.Products = new HashSet<OrderProduct>();
        }

        public string CreatorId { get; set; }

        public virtual ApplicationUser Creator { get; set; }

        public OrderStatus Status { get; set; }

        public virtual ICollection<OrderProduct> Products { get; set; }
    }
}
