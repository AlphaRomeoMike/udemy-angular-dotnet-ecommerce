using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
   public class ProductBrand : BaseEntity
   {
      [MaxLength(100)]
      public string Name { get; set; }
   }
}