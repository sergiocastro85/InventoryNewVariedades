using Microsoft.EntityFrameworkCore;
using PapeleriaInventary.Domain.Entities;

namespace PapeleriaInventary.Infrastructure.Persistence
{
    /// <summary>
    /// Contexto de base de datos para la gestión del inventario de papelería.
    /// Hereda de DbContext y expone los DbSet necesarios para la aplicación.
    /// </summary>
    public class InventoryDbContext : DbContext
    {
        /// <summary>
        /// Constructor que recibe las opciones de configuración del contexto.
        /// </summary>
        /// <param name="options">Opciones de configuración para el contexto de base de datos.</param>
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options)
        {

        }

        /// <summary>
        /// Conjunto de entidades de productos en el inventario.
        /// </summary>
        public DbSet<Product> Products { get; set; }

        /// <summary>
        /// Configura el modelo de entidades utilizando el ModelBuilder.
        /// Se pueden agregar configuraciones personalizadas aquí.
        /// </summary>
        /// <param name="modelBuilder">Constructor de modelos de EF Core.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
