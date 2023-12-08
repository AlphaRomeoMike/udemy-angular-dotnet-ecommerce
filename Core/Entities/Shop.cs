namespace Core.Entities
{
    public class Shop : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Identifier { get; set; }
        public string Password { get; set; }
        public ICollection<Vendor> Vendors { get; set; } = new List<Vendor>();
    }
}