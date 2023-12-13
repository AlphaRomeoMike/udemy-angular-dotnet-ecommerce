using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Core.Entities

{
    [Index(nameof(Shop.Identifier), IsUnique = true)]
    public class Shop : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Identifier { get; set; }
        public bool IsActive { get; set; }
        public bool IsBanned { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual ICollection<Vendor> Vendors { get; set; }
    }
}