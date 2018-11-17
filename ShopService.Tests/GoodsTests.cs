using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using ShopService.Common.Enums;
using ShopService.Common.Exceptions;
using ShopService.Common.Infrastructure;
using ShopService.Common.Models;
using ShopService.Data.Db;
using ShopService.Services.Handlers;
using ShopService.Services.Query;
using ShopService.Services.Responses;
using Xunit;

namespace ShopService.Tests
{
    public class GoodsTests
    {
        private readonly DbContextOptions<ShopContext> _dbContextOptions;

        public GoodsTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<ShopContext>()
                .UseInMemoryDatabase(databaseName: "shopService")
                .Options;

            var goods = new List<Goods>()
            {
                new Goods()
                {
                    Name = "Iphone 10",
                    Currency = Currency.UAH,
                    Price = 10000,
                    GoodsCategories = new List<GoodsCategory>()
                    {
                        new GoodsCategory()
                        {
                            Category = new Category()
                            {
                                Name = "Phones"
                            }
                        }
                    }
                }
            };

            var shopContext = new ShopContext(_dbContextOptions);
            using (shopContext)
            {
                shopContext.Goods.AddRange(goods);
                shopContext.SaveChanges();
            }
        }

        [Fact]
        public async void GetGoodsTest()
        {
            using (var context = new ShopContext(_dbContextOptions))
            {
                var goodsQueryHandler = new GoodsQueryHandlers(context);

                var result = await goodsQueryHandler.Handle(new GetGoodsQuery(),CancellationToken.None);

                Assert.NotNull(result);
                Assert.IsType<QueryResult<GoodsResponse>>(result);
            }
        }

        [Fact]
        public async void GetGoodsByIdTest()
        {
            using (var context = new ShopContext(_dbContextOptions))
            {
                var goodsQueryHandler = new GoodsQueryHandlers(context);

                var result = await goodsQueryHandler.Handle(new GetGoodsById()
                {
                    Id = context.Goods.First().Id,
                }, CancellationToken.None);

                Assert.NotNull(result);
                Assert.IsType<GoodsResponse>(result);
                Assert.Equal(result.Id, context.Goods.First().Id);
            }
        }

        [Fact]
        public async void GetCategoryByIdFalseTest()
        {
            using (var context = new ShopContext(_dbContextOptions))
            {
                var goodsQueryHandler = new GoodsQueryHandlers(context);

                await Assert.ThrowsAnyAsync<DomainException>((async () => await goodsQueryHandler.Handle(new GetGoodsById()
                {
                    Id = context.Goods.Max(x => x.Id) + 1,
                }, CancellationToken.None)));
            }
        }
    }
}
