namespace FoodForYou.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using FoodForYou.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.Products = new HashSet<Product>();
        }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
