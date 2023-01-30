using Core.Entities;

namespace Core.Specifications
{
   public class ProductWithTypeAndBrandSpecification : BaseSpecification<Product>
   {
      private string sort;

      public ProductWithTypeAndBrandSpecification()
      {
         AddInclude(x => x.ProductType);
         AddInclude(x => x.ProductBrand);
      }

      public ProductWithTypeAndBrandSpecification(int? id) : base(x => x.Id == id)
      {
         AddInclude(x => x.ProductType);
         AddInclude(x => x.ProductBrand);
      }

      public ProductWithTypeAndBrandSpecification(string sort)
      {
         AddInclude(x => x.ProductType);
         AddInclude(x => x.ProductBrand);
         AddOrderBy(x => x.Name);
      }
   }
}