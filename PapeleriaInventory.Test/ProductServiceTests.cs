using Xunit;
using Moq;
using PapeleriaInventary.Domain.Entities;
using PapeleriaInventary.Domain.Interfaces;
using PapeleriaInventary.Application.Services;
using System.Threading.Tasks;
using PapeleriaInventary.Infrastructure.UnitOfWork;

namespace PapeleriaInventory.Test
{
    public class ProductServiceTests
    {
        [Fact]
        public async Task AddAsync_ValidProduct_ShouldCallRepository()
        {
            // Crear un producto de prueba con datos válidos
            var product = new Product
            {
                Name = "Lapicero",
                Category = "Escritura",
                Price = 1200,
                Stock = 50
            };

            // Crear mocks para el repositorio y el UnitOfWork
            var repoMock = new Mock<IRepository<Product>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            // Configurar el mock del UnitOfWork para que devuelva el mock del repositorio
            unitOfWorkMock
                .Setup(u => u.Products)
                .Returns(repoMock.Object);

            // Crear el servicio de productos usando el mock del UnitOfWork
            var service = new ProductService(unitOfWorkMock.Object);

            // Llamar al método AddAsync del servicio (no se espera el Task, posible error)
            service.AddAsync(product);

            // Verificar que el método AddAsync del repositorio fue llamado una vez con el producto
            repoMock.Verify(r => r.AddAsync(product), Times.Once);
            // Verificar que el método CompleteAsync del UnitOfWork fue llamado una vez
            unitOfWorkMock.Verify(u => u.CompleteAsync(), Times.Once);
        }
    }
}
