using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PapeleriaInventary.Infrastructure.Persistence
{
    public class InventoryDbContextFactory : IDesignTimeDbContextFactory<InventoryDbContext>
    {
        public InventoryDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<InventoryDbContext>();

            optionsBuilder.UseMySql(
                "Server=yamabiko.proxy.rlwy.net;Port=52729;Database=railway;Uid=root;Pwd=hmCsmOniALjojItcWPAwrLZQaGONaYeT;",
                new MySqlServerVersion(new Version(8, 0, 36)),
                mySqlOptions =>
                {
                    mySqlOptions.EnableRetryOnFailure();
                });

            return new InventoryDbContext(optionsBuilder.Options);
        }
    }
}

