using PapeleriaInventary.Domain.Entities;


namespace PapeleriaInventary.Application.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync(string? search, string? category);

        Task<Product?> GetByIdAsync(int id);

        Task AddAsync(Product product);

        Task UpdateAsync(Product product);

        Task DeleteAsync(int id);

    }
}
