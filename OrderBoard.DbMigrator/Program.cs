using System;
using Microsoft.EntityFrameworkCore;

namespace OrderBoard.DbMigrator
{
    class Program
    {
        public static async Task Main(string[] args)
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
