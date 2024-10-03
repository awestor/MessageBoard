using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using OrderBoard.AppServices.Adverts.Repositories;
using OrderBoard.AppServices.Adverts.Services;
using OrderBoard.AppServices.Categories.Repositories;
using OrderBoard.AppServices.Categories.Services;
using OrderBoard.AppServices.User.Services;
using OrderBoard.ComponentRegistrator.MapProfiles;
using OrderBoard.DataAccess.Repositories;
using OrderBoard.Infrastructure.Repository;

namespace OrderBoard.ComponentRegistrator
{
    public static class ComponentRegistrator
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //services.AddTransient<IUserService, UserService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IAdvertService, AdvertService>();

            //object value = ();
            //FAKE REPO
            //services.AddSingleton<IUserRepository, UserRepository>();
            services.AddScoped<IAdvertRepository, AdvertRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

            services.AddSingleton<IMapper>(new Mapper(GetMapperConfiguration()));

            return services;
        }

        private static MapperConfiguration GetMapperConfiguration()
        {
            var configuration = new MapperConfiguration(configure =>
            {
                configure.AddProfile<CategoryProfile>();
            });
            configuration.AssertConfigurationIsValid();
            return configuration;
        }
    }
}