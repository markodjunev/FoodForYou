namespace FoodForYou.Data.Models
{
    using System.Collections.Generic;

    using FoodForYou.Data.Common.Models;

    public class Order : BaseDeletableModel<int>
    {
        public Order()
        {
            this.Products = new HashSet<OrderProduct>();
        }

        public string Address { get; set; }

        public string CreatorId { get; set; }

        public virtual ApplicationUser Creator { get; set; }

        public virtual ICollection<OrderProduct> Products { get; set; }
    }
}
