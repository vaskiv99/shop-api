using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ShopService.Common.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<Basket> Baskets { get; set; } = new List<Basket>();
    }
}
