using Microsoft.EntityFrameworkCore;
using OrderBoard.Domain;

namespace OrderBoard.DataAccess.Configurations
{
    public class AdvertConfiguration : IEntityTypeConfiguration<Advert>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Advert> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(256).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(1024).IsRequired();
        }
    }
}
