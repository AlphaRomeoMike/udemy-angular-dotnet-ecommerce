using Core.Entities;

namespace Core.Specifications
{
   public class ProductWithTypeAndBrandSpecification : BaseSpecification<Product>
   {
      public ProductWithTypeAndBrandSpecification()
      {
        AddInclude(x => x.ProductType);
        AddInclude(x => x.ProductBrand);
      }
   }
}