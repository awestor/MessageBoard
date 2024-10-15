using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OrderBoard.Domain.Entities;


namespace OrderBoard.DataAccess.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ItemId).IsRequired();
            builder.Property(x => x.OrderPrice);
            builder.Property(x => x.OrderId).IsRequired();
            builder.Property(x => x.Count).IsRequired();           
        }
    }
}