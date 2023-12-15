using Core.Entities;

namespace Core.Specifications
{
    public class VendorWithShopSpecification : BaseSpecification<Vendor>
    {
        public VendorWithShopSpecification()
        {
            AddInclude(x => x.ShopId);
        }

        public VendorWithShopSpecification(int? id) : base (x => x.Id == id)
        {
            AddInclude(x => x.ShopId);
        }

        public VendorWithShopSpecification(VendorSpecParams spec)
        :base(x => 
        (string.IsNullOrEmpty(spec.Search) || x.Name.ToLower().Contains(spec.Search)) &&
        (!spec.ShopId.HasValue) || x.ShopId == spec.ShopId)
        {
            AddInclude(x => x.Shop);
            AddOrderBy(x => x.Name);
            ApplyPaging(spec.PageSize * (spec.PageIndex - 1), spec.PageSize);

            if (!string.IsNullOrEmpty(spec.Sort))
            {
                switch (spec.Sort)
                {
                    case "nameAsc":
                        AddOrderBy(v => v.Name);
                        break;
                    case "nameDesc":
                        AddOrderByDescending(v => v.Name);
                        break;
                    case "idAsc":
                        AddOrderBy(v => v.Identifier);
                        break;
                    case "idDesc":
                        AddOrderByDescending(v => v.Identifier);
                        break;
                    default:
                        AddOrderBy(v => v.Name);
                        break;
                }
            }
        }
    }
}