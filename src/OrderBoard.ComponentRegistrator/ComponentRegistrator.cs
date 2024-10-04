using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using OrderBoard.AppServices.Items.Repositories;
using OrderBoard.AppServices.Items.Services;
using OrderBoard.AppServices.Categories.Repositories;
using OrderBoard.AppServices.Categories.Services;
using OrderBoard.AppServices.User.Services;
using OrderBoard.AppServices.Users.Repository;
using OrderBoard.AppServices.Users.Services;
using OrderBoard.ComponentRegistrator.MapProfiles;
using OrderBoard.DataAccess.Repositories;
using OrderBoard.Infrastructure.Repository;
using OrderBoard.AppServices.Repository.Services;
using OrderBoard.AppServices.Repository.Repository;

namespace OrderBoard.ComponentRegistrator
{
    public static class ComponentRegistrator
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<IOrderItemService, OrderItemService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();

            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

            services.AddSingleton<IMapper>(new Mapper(GetMapperConfiguration()));

            return services;
        }

        private static MapperConfiguration GetMapperConfiguration()
        {
            var configuration = new MapperConfiguration(configure =>
            {
                configure.AddProfile<CategoryProfile>();
                configure.AddProfile<ItemProfile>();
                configure.AddProfile<UserProfile>();
                configure.AddProfile<OrderItemProfile>();
            });
            configuration.AssertConfigurationIsValid();
            return configuration;
        }
    }
}