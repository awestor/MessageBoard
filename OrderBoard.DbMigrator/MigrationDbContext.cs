using Microsoft.EntityFrameworkCore;
using OrderBoard.DataAccess;

namespace OrderBoard.DbMigrator
{
    public class MigrationDbContext: OrderBoardDbContext
    {
        public MigrationDbContext(DbContextOptions option):base(option){ }
    }
}
