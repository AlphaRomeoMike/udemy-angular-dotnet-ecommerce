using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(int? id);
        Task<List<Product>> GetProducts();
    }
}