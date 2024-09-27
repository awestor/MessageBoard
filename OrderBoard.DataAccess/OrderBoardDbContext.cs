using Microsoft.EntityFrameworkCore;
using OrderBoard.DataAccess.Configurations;

namespace OrderBoard.DataAccess
{
    public class OrderBoardDbContext : DbContext
    {
        public OrderBoardDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new AdvertConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        }
    }
}
