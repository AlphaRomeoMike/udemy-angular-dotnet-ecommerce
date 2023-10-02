using Core.Entities;

namespace Core.Specifications
{
    public class ProductTypeSpecification : BaseSpecification<ProductType>
    {
        public ProductTypeSpecification(ProductTypeSpecParam productTypeSpec) : base(x =>
        (string.IsNullOrEmpty(productTypeSpec.Search) || x.Name.ToLower().Contains(productTypeSpec.Search)))
        {
            ApplyPaging(productTypeSpec.PageSize * (productTypeSpec.PageIndex - 1), productTypeSpec.PageSize);
            if (!string.IsNullOrEmpty(productTypeSpec.Sort))
            {
                switch (productTypeSpec.Sort)
                {
                    case "nameAsc":
                        AddOrderBy(p => p.Name);
                        break;
                    case "nameDesc":
                        AddOrderByDescending(p => p.Name);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}