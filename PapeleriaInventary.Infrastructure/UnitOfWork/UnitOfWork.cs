using PapeleriaInventary.Domain.Entities;
using PapeleriaInventary.Domain.Interfaces;
using PapeleriaInventary.Infrastructure.Persistence;
using PapeleriaInventary.Infrastructure.Repositories;

namespace PapeleriaInventary.Infrastructure.UnitOfWork
{
    // Clase que coordina los repositorios y guarda los cambios en la base de datos
    public class UnitOfWork : IUnitOfWork
    {
        private readonly InventoryDbContext _context; // Contexto de base de datos
        private IRepository<Product> _products; // Repositorio de productos

        // Recibe el contexto de base de datos al crear la unidad de trabajo
        public UnitOfWork(InventoryDbContext context)
        {
            _context = context;
        }

        // Devuelve el repositorio de productos, lo crea si no existe
        public IRepository<Product> Products => _products ??= new Repository<Product>(_context);

        // Guarda los cambios pendientes en la base de datos de forma asíncrona
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        // Libera los recursos del contexto de base de datos
        public void Dispose()
        {
            _context.Dispose();
        }


    }
}
