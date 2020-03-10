namespace FoodForYou.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using FoodForYou.Data.Common.Models;

    public class Review : BaseDeletableModel<int>
    {
        public string Text { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public string CreatorId { get; set; }

        public virtual ApplicationUser Creator { get; set; }
    }
}
