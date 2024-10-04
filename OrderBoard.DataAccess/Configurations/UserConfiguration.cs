using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderBoard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderBoard.DataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<EntUser>
    {
        public void Configure(EntityTypeBuilder<EntUser> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Email).HasMaxLength(256).IsRequired();
            builder.Property(x => x.Password).HasMaxLength(128).IsRequired();
            builder.Property(x => x.PhoneNumber).HasMaxLength(48);
            builder.Property(x => x.Login).HasMaxLength(256).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(4096);

            builder.HasMany(x => x.Items).WithOne(x => x.User).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}