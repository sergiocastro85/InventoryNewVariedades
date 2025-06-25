using Microsoft.EntityFrameworkCore;
using PapeleriaInventary.Domain.Interfaces;
using PapeleriaInventary.Infrastructure.Persistence;
using System.Linq.Expressions;

namespace PapeleriaInventary.Infrastructure.Repositories
{
    /// <summary>
    /// Implementación genérica del repositorio para operaciones CRUD sobre entidades.
    /// Utiliza Entity Framework Core para interactuar con la base de datos.
    /// </summary>
    /// <typeparam name="TEntity">Tipo de la entidad sobre la que opera el repositorio.</typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Contexto de base de datos protegido para uso en clases derivadas.
        /// </summary>
        protected readonly InventoryDbContext _context;

        /// <summary>
        /// DbSet protegido para la entidad TEntity.
        /// </summary>
        protected readonly DbSet<TEntity> _set;

        /// <summary>
        /// Constructor que inicializa el contexto y el DbSet.
        /// </summary>
        /// <param name="context">Contexto de base de datos.</param>
        public Repository(InventoryDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _set = context.Set<TEntity>();
        }

        /// <inheritdoc/>
        public async Task<TEntity?> GetByIdAsync(int id)
        {
            // Busca una entidad por su identificador primario.
            return await _set.FindAsync(id);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            // Obtiene todas las entidades del DbSet.
            return await _set.ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            // Busca entidades que cumplan con el predicado especificado.
            return await _set.Where(predicate).ToListAsync();
        }

        /// <inheritdoc/>
        public async Task AddAsync(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            // Agrega una nueva entidad al DbSet.
            await _set.AddAsync(entity);
        }

        /// <inheritdoc/>
        public void Update(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            // Marca la entidad como modificada.
            _set.Update(entity);
        }

        /// <inheritdoc/>
        public void Delete(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            // Elimina la entidad del DbSet.
            _set.Remove(entity);
        }
    }
}
