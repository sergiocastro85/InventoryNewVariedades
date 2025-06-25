using PapeleriaInventary.Domain.Entities;
using PapeleriaInventary.Domain.Interfaces;

namespace PapeleriaInventary.Application.Services
{
    // Servicio que implementa la lógica de negocio para productos.  
    public class ProductService : IProductService
    {

        private readonly IUnitOfWork _unitOfWork; // Campo para acceder a la unidad de trabajo.  

        // Constructor que recibe la unidad de trabajo por inyección de dependencias.  
        public ProductService( IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork; // Asigna la unidad de trabajo al campo privado.  

        }

        // Obtiene todos los productos, con filtros opcionales por búsqueda y categoría.  
        public async Task<IEnumerable<Product>> GetAllAsync(string? search, string? category)
        {
            var products = await _unitOfWork.Products.GetAllAsync(); // Obtiene todos los productos.  

            if (!string.IsNullOrEmpty(search)) // Si hay un término de búsqueda...  
            {
                // Filtra los productos cuyo nombre contiene el texto buscado (ignorando mayúsculas/minúsculas).  
                products = products.Where(p => p.Name.Contains(search, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(category)) // Si hay una categoría especificada...  
            {
                // Filtra los productos que pertenecen exactamente a esa categoría (ignorando mayúsculas/minúsculas).  
                products = products.Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
            }

            return products; // Devuelve la lista filtrada de productos.  
        }

        // Obtiene un producto por su identificador.  
        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _unitOfWork.Products.GetByIdAsync(id); // Busca el producto por ID.  
        }

        // Agrega un nuevo producto al inventario.  
        public async Task AddAsync(Product product)
        {
            await _unitOfWork.Products.AddAsync(product); // Agrega el producto al repositorio.  
            await _unitOfWork.CompleteAsync(); // Guarda los cambios en la base de datos.  
        }

        // Actualiza un producto existente.  
        public async Task UpdateAsync(Product product)
        {
            _unitOfWork.Products.Update(product); // Marca el producto como modificado.  
            await _unitOfWork.CompleteAsync(); // Guarda los cambios en la base de datos.  
        }

        // Elimina un producto por su identificador.  
        public async Task DeleteAsync(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id); // Busca el producto por ID.  
            if (product != null) // Si el producto existe...  
            {
                _unitOfWork.Products.Delete(product); // Lo elimina del repositorio.  
                await _unitOfWork.CompleteAsync(); // Guarda los cambios en la base de datos.  
            }
        }
    }
}
