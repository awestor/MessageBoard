using Microsoft.EntityFrameworkCore;


namespace OrderBoard.DbMigrator
{
    public static class ServiseCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigurationDbConnection(configuration);
            return services;
        }
        private static IServiceCollection ConfigurationDbConnection(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ConnectionString");
            services.AddDbContext<MigrationDbContext>(options => options.UseNpgsql(connectionString));
            return services;
        }
    }
}
