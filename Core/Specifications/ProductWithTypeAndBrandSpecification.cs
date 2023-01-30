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

      public ProductWithTypeAndBrandSpecification(int? id) : base(x => x.Id == id)
      {
         AddInclude(x => x.ProductType);
         AddInclude(x => x.ProductBrand);
      }

      public ProductWithTypeAndBrandSpecification(ProductSpecPrams productSpec)
         : base(x =>
            (string.IsNullOrEmpty(productSpec.Search) || x.Name.ToLower().Contains(productSpec.Search)) &&
            (!productSpec.BrandId.HasValue || x.ProductBrandId == productSpec.BrandId) &&
            (!productSpec.TypeId.HasValue || x.ProductTypeId == productSpec.TypeId))
      {
         AddInclude(x => x.ProductType);
         AddInclude(x => x.ProductBrand);
         AddOrderBy(x => x.Name);
         ApplyPaging(productSpec.PageSize * (productSpec.PageIndex - 1), productSpec.PageSize);
         if (!string.IsNullOrEmpty(productSpec.Sort))
         {
            switch (productSpec.Sort)
            {
               case "priceAsc":
                  AddOrderBy(p => p.Price);
                  break;
               case "priceDesc":
                  AddOrderByDescending(p => p.Price);
                  break;
               default:
                  AddOrderBy(p => p.Name);
                  break;
            }
         }
      }
   }
}