using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.EntityFrameworkCore;
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
    public class CategoryTests
    {
        private readonly DbContextOptions<ShopContext> _dbContextOptions;

        public CategoryTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<ShopContext>()
                .UseInMemoryDatabase(databaseName: "shopService")
                .Options;

            var categories = new List<Category>()
            {
                new Category()
                {
                    Name = "TV"
                },
                new Category()
                {
                    Name = "Phones"
                }
            };

            var shopContext = new ShopContext(_dbContextOptions);
            using (shopContext)
            {
                shopContext.Categories.AddRange(categories);
                shopContext.SaveChanges();
            }
        }

        [Fact]
        public async void GetCategoriesTest()
        {
            using (var context = new ShopContext(_dbContextOptions))
            {
                var categoryQueryHandler = new CategoryQueryHandler(context);

                var result = await categoryQueryHandler.Handle(new GetCategories(), CancellationToken.None);

                Assert.NotNull(result);
                Assert.IsType<QueryResult<CategoryResponse>>(result);
            }
        }

        [Fact]
        public async void GetCategoryByIdTest()
        {
            using (var context = new ShopContext(_dbContextOptions))
            {
                var categoryQueryHandler = new CategoryQueryHandler(context);

                var result = await categoryQueryHandler.Handle(new GetCategoryById()
                {
                    Id = context.Categories.First().Id,
                }, CancellationToken.None);

                Assert.NotNull(result);
                Assert.IsType<CategoryWithGoodsResponse>(result);
                Assert.Equal(result.Id, context.Categories.First().Id);
            }
        }

        [Fact]
        public async void GetCategoryByIdFalseTest()
        {
            using (var context = new ShopContext(_dbContextOptions))
            {
                var categoryQueryHandler = new CategoryQueryHandler(context);

                await Assert.ThrowsAnyAsync<DomainException>((async () => await categoryQueryHandler.Handle(new GetCategoryById()
                {
                    Id = context.Categories.Max(x => x.Id) + 1,
                }, CancellationToken.None)));
            }
        }
    }
}

