namespace API.DTOs
{
    class VendorDto 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Identifier { get; set; }
        public string Password { get; set; }
        public bool IsOwner { get; set; } = false;
        public bool IsActive { get; set; } = false;
        public int ShopId { get; set; }
    }
}