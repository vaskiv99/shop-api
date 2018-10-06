using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopService.Data.Db;

namespace ShopService.Data.Services
{
    public class DatabaseInitializer
    {
        private readonly ShopContext _db;
        private readonly ILogger _logger;

        public DatabaseInitializer(ShopContext context, ILoggerFactory loggerFactory)
        {
            _db = context;
            _logger = loggerFactory.CreateLogger(GetType());
        }

        public void Migrate()
        {
            try
            {
                _db.Database.Migrate();
            }
            catch (Exception e)
            {
                _logger.LogWarning(e, "Database Migration Failed");
                throw;
            }
        }
    }
}
