using Microsoft.EntityFrameworkCore;
using OrderBoard.DbMigrator;

namespace OrderBoard.DbMigrator
{
    public class Program
    {
        public static async void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args).ConfigureServices((HostBuilderContext, services) =>
            {
                services.AddServices(HostBuilderContext.Configuration);
            }).Build();

            await host.RunAsync();
        }
        private static async Task MigrateAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetService<MigrationDbContext>();
            await context!.Database.MigrateAsync();
        }
    }
}
