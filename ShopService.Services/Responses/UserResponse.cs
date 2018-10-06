using System.Collections.Generic;

namespace ShopService.Services.Responses
{
    public class UserResponse
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
    }

    public class UserBasketResponse : UserResponse
    {
        public List<Item> Items { get; set; } = new List<Item>();
    }

    public class Item
    {
        public GoodsResponse Goods { get; set; }

        public long Count { get; set; }
    }
}
