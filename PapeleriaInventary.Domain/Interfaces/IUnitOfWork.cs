using PapeleriaInventary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PapeleriaInventary.Domain.Interfaces
{
    /// <summary>
    /// Representa el patrón de unidad de trabajo, encapsulando la lógica para coordinar y garantizar
    /// que los cambios realizados en múltiples repositorios se guarden de forma coherente en la base de datos.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Repositorio para gestionar las operaciones sobre productos.
        /// </summary>
        IRepository<Product> Products { get; }

        /// <summary>
        /// Guarda los cambios pendientes de forma asíncrona y retorna la cantidad de registros afectados.
        /// </summary>
        Task<int> CompleteAsync();

    }
}
