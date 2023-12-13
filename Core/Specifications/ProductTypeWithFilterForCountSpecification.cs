using Core.Entities;

namespace Core.Specifications
{
   public class ProductTypeWithFiltersForCountSpecification : BaseSpecification<ProductType>
   {
      public ProductTypeWithFiltersForCountSpecification(ProductTypeSpecParam productSpec)
        : base(x =>
            (string.IsNullOrEmpty(productSpec.Search) || x.Name.ToLower().Contains(productSpec.Search)))
      {
      }
   }
}