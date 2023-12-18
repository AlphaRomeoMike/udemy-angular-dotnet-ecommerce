using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Core.Entities
{
    [Index(nameof(Identifier), IsUnique = true)]
    public class Vendor : BaseEntity
    {
        [MaxLength(100, ErrorMessage = "Name cannot be greater than 100")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Identifier { get; set; }
        public string Password { get; set; }
        public bool IsOwner { get; set; } = false;
        public bool IsActive { get; set; } = false;
        public int ShopId { get; set; }
        public Shop Shop { get; set; }
    }
}