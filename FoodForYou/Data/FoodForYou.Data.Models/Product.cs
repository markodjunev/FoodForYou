namespace FoodForYou.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using FoodForYou.Data.Common.Models;

    public class Product : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        // public virtual ICollection<Review> Reviews { get; set; }
    }
}
