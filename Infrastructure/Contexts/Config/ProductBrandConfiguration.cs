using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Contexts.Config
{
   public class ProductBrandConfiguration : IEntityTypeConfiguration<ProductBrand>
   {
      public void Configure(EntityTypeBuilder<ProductBrand> builder)
      {
      }
   }
}