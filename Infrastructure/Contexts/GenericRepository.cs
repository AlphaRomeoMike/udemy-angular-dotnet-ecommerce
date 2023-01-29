using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts
{
   public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
   {
      private readonly StoreContext _context;
      public GenericRepository(StoreContext context)
      {
         _context = context;
      }

      public async Task<T> GetByIdAsync(int? id)
      {
         return await _context.Set<T>().FindAsync(id);
      }

      public async Task<List<T>> ListAllAsync()
      {
         return await _context.Set<T>().ToListAsync();
      }
   }
}