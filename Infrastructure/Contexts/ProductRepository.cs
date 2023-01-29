using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts
{
    #nullable disable
   public class ProductRepository : IProductRepository
   {
      private readonly StoreContext _context;

      public ProductRepository(StoreContext context)
      {
         _context = context;
      }

      public async Task<Product> GetProductByIdAsync(int? id)
      {
        return await _context.Products.FindAsync(id);
      }

      public async Task<List<Product>> GetProducts()
      {
         return await _context.Products.ToListAsync();
      }
   }
}