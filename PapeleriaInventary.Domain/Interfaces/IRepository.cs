using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PapeleriaInventary.Domain.Interfaces
{
    /// <summary>
    /// Interfaz genérica para operaciones CRUD sobre entidades.
    /// Define los métodos básicos para acceder y manipular datos de cualquier entidad.
    /// </summary>
    /// <typeparam name="TEntity">Tipo de la entidad sobre la que se opera.</typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Obtiene una entidad por su identificador único de forma asíncrona.
        /// </summary>
        /// <param name="id">Identificador de la entidad.</param>
        /// <returns>Entidad encontrada o null si no existe.</returns>
        Task<TEntity> GetByIdAsync(int id);

        /// <summary>
        /// Obtiene todas las entidades de forma asíncrona.
        /// </summary>
        /// <returns>Lista de todas las entidades.</returns>
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Busca entidades que cumplan con un predicado especificado de forma asíncrona.
        /// </summary>
        /// <param name="predicate">Expresión lambda para filtrar entidades.</param>
        /// <returns>Lista de entidades que cumplen la condición.</returns>
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Agrega una nueva entidad de forma asíncrona.
        /// </summary>
        /// <param name="entity">Entidad a agregar.</param>
        Task AddAsync(TEntity entity);

        /// <summary>
        /// Actualiza una entidad existente.
        /// </summary>
        /// <param name="entity">Entidad a actualizar.</param>
        void Update(TEntity entity);

        /// <summary>
        /// Elimina una entidad existente.
        /// </summary>
        /// <param name="entity">Entidad a eliminar.</param>
        void Delete(TEntity entity);
    }
}
