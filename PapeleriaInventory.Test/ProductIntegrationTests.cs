using Xunit;
using Microsoft.EntityFrameworkCore;
using PapeleriaInventary.Domain.Entities;
using PapeleriaInventary.Infrastructure.Persistence;
using PapeleriaInventary.Infrastructure.Repositories;
using PapeleriaInventary.Infrastructure.UnitOfWork;
using PapeleriaInventary.Application.Services;


namespace PapeleriaInventory.Test
{
    public class ProductIntegrationTests
    {
        [Fact]
        public async Task AddAsync_Product_ShouldBePersisted()
        {
            
            var options = new DbContextOptionsBuilder<InventoryDbContext>()
                                                .UseInMemoryDatabase(databaseName: "IntegrationTestDb")
                                                .Options;

            await using var context = new InventoryDbContext(options);

            var unitOfWork = new UnitOfWork(context);

            var service = new ProductService(unitOfWork);

            var product = new Product
            {
                Name = "Test Product",
                Category = "Test Category",
                Price = 1000,
                Stock = 10
            };

            await service.AddAsync(product);

            var exists = await context.Products.AnyAsync(p => p.Name == "Test Product");

            Assert.True(exists);
        }
    }
}
