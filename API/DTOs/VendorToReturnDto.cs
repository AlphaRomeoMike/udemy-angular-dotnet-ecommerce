using Core.Entities;

namespace API.DTOs {
    public class VendorToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Identifier { get; set; }
        public bool IsOwner { get; set; } = false;
        public bool IsActive { get; set; } = false;
        public ShopToReturnDto Shop { get; set; }
    }
}