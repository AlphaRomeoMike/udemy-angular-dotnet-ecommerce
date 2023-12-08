namespace Core.Entities
{
    public class Vendor : BaseEntity
    {
        public String Name { get; set; }
        public String Description { get; set; }
        public String Identifier { get; set; }
        public String Password { get; set; }
        public bool IsOwner { get; set; }
        public bool IsActive { get; set; }
        public int ShopId { get; set; }
        public Shop Shop { get; set; }
    }
}