using System;
using ShopService.Common.Enums;
using ShopService.Common.Models;

namespace ShopService.Tests
{
    public static class DbSeeder
    {
        public static User User => new User()
        {
            Id = Guid.NewGuid().ToString(),
            Email = "vaskiv99@ukr.net",
            AccessFailedCount = 0,
            EmailConfirmed = true,
            FirstName = "Vasul",
            LastName = "Vaskiv",
            LockoutEnabled = false,
        };

        public static Goods Goods => new Goods()
        {
            Currency = Currency.UAH,
            Name = "IPhone 7",
            Price = 12000
        };
    }
}
