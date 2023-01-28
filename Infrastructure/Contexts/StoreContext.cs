using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts
{
   #nullable disable
   public class StoreContext : DbContext
   {
      public StoreContext(DbContextOptions options) : base(options)
      {

      }

      public DbSet<Product> Products { get; set; }
   }
}