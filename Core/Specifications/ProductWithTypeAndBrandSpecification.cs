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

      public ProductWithTypeAndBrandSpecification(int? id) : base (x => x.Id == id)
      {
        AddInclude(x => x.ProductType);
        AddInclude(x => x.ProductBrand);
      }
   }
}