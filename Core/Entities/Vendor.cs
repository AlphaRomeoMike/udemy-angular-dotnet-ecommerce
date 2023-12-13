using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Core.Entities
{
    [Index(nameof(Vendor.Identifier), IsUnique = true)]
    public class Vendor : BaseEntity
    {
        [MaxLength(100, ErrorMessage = "Name cannot be greater than 100")]
        public String Name { get; set; }
        public String Description { get; set; }
        public String Identifier { get; set; }
        public String Password { get; set; }
        public bool IsOwner { get; set; } = false;
        public bool IsActive { get; set; } = false;
        public int ShopId { get; set; }
        public Shop Shop { get; set; }
    }
}