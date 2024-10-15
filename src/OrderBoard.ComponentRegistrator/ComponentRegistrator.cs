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
using OrderBoard.AppServices.Orders.Services;
using OrderBoard.AppServices.Orders.Repository;
using OrderBoard.AppServices.Files.Services;
using OrderBoard.AppServices.Files.Repositories;
using OrderBoard.AppServices.Items.SpecificationContext.Builders;
using OrderBoard.AppServices.OrderItems.SpecificationContext.Builders;
using OrderBoard.AppServices.Orders.SpecificationContext.Builders;
using OrderBoard.AppServices.Categories.SpecificationContext.Builders;
using FluentValidation;
using OrderBoard.AppServices.Users.SpecificationContext.Builders;
using FluentValidation.AspNetCore;
using OrderBoard.Infrastructure.Services.Logging;
using OrderBoard.AppServices.Other.Validators.Categorys;
using OrderBoard.AppServices.Other.Validators.Items;
using OrderBoard.AppServices.Other.Validators.ItemValidator;
using OrderBoard.AppServices.Other.Validators.OrderItems;
using OrderBoard.AppServices.Other.Validators.Orders;
using OrderBoard.AppServices.Other.Validators.Users;
using OrderBoard.AppServices.Other.Services;
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
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IFileService, FileService>();

            

            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            services.AddScoped<IFileRepository, FileRepository>();


            services.AddScoped<IItemSpecificationBuilder, ItemSpecificationBuilder>();
            services.AddScoped<IOrderItemSpecificationBuilder, OrderItemSpecificationBuilder>();
            services.AddScoped<IOrderSpecificationBuilder, OrderSpecificationBuilder>();
            services.AddScoped<ICategorySpecificationBuilder, CategorySpecificationBuilder>();
            services.AddScoped<IUserSpecificationBuilder, UserSpecificationBuilder>();

            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

            services.AddSingleton<IMapper>(new Mapper(GetMapperConfiguration()));

            services.AddScoped<IStructuralLoggingService, StructuralLoggingService>();

            return services;
        }
        public static IServiceCollection AddFluentValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<CreateCategoryValidator>();
            services.AddValidatorsFromAssemblyContaining<SearchCategoryValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateCategoryValidator>();
            services.AddValidatorsFromAssemblyContaining<CreateItemValidator>();
            services.AddValidatorsFromAssemblyContaining<SearchItemByNameValidator>();
            services.AddValidatorsFromAssemblyContaining<SearchItemByUserIdValidator>();
            services.AddValidatorsFromAssemblyContaining<SearchItemForPaginationValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateItemValidator>();
            services.AddValidatorsFromAssemblyContaining<CreateOrderItemValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateOrderItemValidator>();
            services.AddValidatorsFromAssemblyContaining<SearchOrderItemFromOrderRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<SearchOrderAuthRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<SearchOrderRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();
            services.AddValidatorsFromAssemblyContaining<EmailAuthValidator>();
            services.AddValidatorsFromAssemblyContaining<LoginAuthValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateUserValidator>();
            services.AddFluentValidationAutoValidation();

            return services;
        }

        private static MapperConfiguration GetMapperConfiguration()
        {
            var configuration = new MapperConfiguration(configure =>
            {
                configure.AddProfile<OrderProfile>();
                configure.AddProfile<CategoryProfile>();
                configure.AddProfile<ItemProfile>();
                configure.AddProfile<UserProfile>();
                configure.AddProfile<OrderItemProfile>();
                configure.AddProfile<FileProfile>();
            });
            configuration.AssertConfigurationIsValid();
            return configuration;
        }
    }
}