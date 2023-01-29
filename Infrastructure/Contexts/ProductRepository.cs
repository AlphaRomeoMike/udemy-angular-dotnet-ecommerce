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

      public async Task<List<ProductBrand>> GetProductBrandsAsync()
      {
         return await _context.ProductBrand.ToListAsync();
      }

      public async Task<Product> GetProductByIdAsync(int? id)
      {
         return await _context.Products
            .Include(p => p.ProductType)
            .Include(p => p.ProductBrand)
            .FirstOrDefaultAsync(p => p.Id == id);
      }

      public async Task<List<Product>> GetProducts()
      {
         return await _context.Products
            .Include(p => p.ProductType)
            .Include(p => p.ProductBrand)
            .ToListAsync();
      }

      public async Task<List<ProductType>> GetProductTypeAsync()
      {
         return await _context.ProductType.ToListAsync();

      }
   }
}