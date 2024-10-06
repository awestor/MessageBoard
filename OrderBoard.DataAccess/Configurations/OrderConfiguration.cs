using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderBoard.Domain.Entities;

namespace OrderBoard.DataAccess.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserId).IsRequired();

            builder.HasMany(x => x.OrderList).WithOne(x => x.Order).HasForeignKey(x => x.Order).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
