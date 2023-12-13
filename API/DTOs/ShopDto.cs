namespace API.DTOs
{
    public class ShopDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Identifier { get; set; }
        public bool IsActive { get; set; }
        public bool IsBanned { get; set; }
    }
}