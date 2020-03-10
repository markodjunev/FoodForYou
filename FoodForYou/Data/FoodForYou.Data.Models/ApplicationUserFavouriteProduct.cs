namespace FoodForYou.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using FoodForYou.Data.Common.Models;

    public class ApplicationUserFavouriteProduct : BaseDeletableModel<int>
    {
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
