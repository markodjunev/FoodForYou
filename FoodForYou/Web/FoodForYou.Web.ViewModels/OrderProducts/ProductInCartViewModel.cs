namespace FoodForYou.Web.ViewModels.OrderProducts
{
    using FoodForYou.Data.Models;
    using FoodForYou.Data.Models.Enums;
    using FoodForYou.Services.Mapping;

    public class ProductInCartViewModel : IMapFrom<OrderProduct>
    {
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public string ProductName { get; set; }

        public string CreatorId { get; set; }

        public virtual ApplicationUser Creator { get; set; }

        public OrderProductStatus Status { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
