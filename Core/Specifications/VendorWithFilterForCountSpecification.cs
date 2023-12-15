using Core.Entities;

namespace Core.Specifications
{
   public class VendorWithFilterForCountSpecification : BaseSpecification<Vendor>
   {
      public VendorWithFilterForCountSpecification(VendorSpecParams vendorSpec)
        : base(x =>
            (string.IsNullOrEmpty(vendorSpec.Search) || x.Name.ToLower().Contains(vendorSpec.Search)) &&
            (!vendorSpec.ShopId.HasValue || x.ShopId == vendorSpec.ShopId))
      {
      }
   }
}