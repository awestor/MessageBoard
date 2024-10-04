using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderBoard.Domain.Entities;

namespace OrderBoard.DataAccess.Configurations
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(512).IsRequired();
            builder.Property(x => x.Count).IsRequired();
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.CategoryId).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(1024).IsRequired();

            builder.HasMany(x => x.OrderItem).WithOne(x => x.Item).HasForeignKey(x => x.ItemId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}