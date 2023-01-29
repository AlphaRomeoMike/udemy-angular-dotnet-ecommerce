using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Contexts.Config
{
   public class ProductConfiguration : IEntityTypeConfiguration<Product>
   {
      public void Configure(EntityTypeBuilder<Product> builder)
      {
         builder.Property(p => p.Name).IsRequired();
         builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
         builder.Property(p => p.PictureUrl).IsRequired();
         builder.HasOne(p => p.ProductBrand).WithMany().HasForeignKey(p => p.ProductBrandId);
         builder.HasOne(p => p.ProductType).WithMany().HasForeignKey(p => p.ProductTypeId);
      }
   }
}