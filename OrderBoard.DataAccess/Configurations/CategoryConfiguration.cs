using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderBoard.Domain.Entities;

namespace OrderBoard.DataAccess.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(256).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(4096).IsRequired();

            builder.HasMany(x => x.Adverts).WithOne(x => x.Category).HasForeignKey(x => x.CategoryId).IsRequired().OnDelete(DeleteBehavior.NoAction);
        }
    }
}
