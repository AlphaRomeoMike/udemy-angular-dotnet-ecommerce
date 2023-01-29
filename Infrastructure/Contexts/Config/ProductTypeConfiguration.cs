using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts.Config
{
   public class ProductTypeConfiguration : IEntityTypeConfiguration<ProductType>
   {
      public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ProductType> builder)
      {
      }
   }
}