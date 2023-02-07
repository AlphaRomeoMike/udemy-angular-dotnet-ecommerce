using Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Contexts.Config
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.ShippedToAddress, a => a.WithOwner());
            builder.Navigation(a => a.ShippedToAddress).IsRequired();
            builder.Property(s => s.Status)
                .HasConversion(o => o.ToString(), o => (OrderStatus) Enum.Parse(typeof(OrderStatus), o));
            builder.HasMany(o => o.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
            builder.Property(i => i.SubTotal).HasColumnType("decimal(18,2)");

        }
    }
}
