using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OrderBoard.Api.Controllers;
using OrderBoard.Api.Middlewares;
using OrderBoard.AppServices.Other.Validators.Categorys;
using OrderBoard.AppServices.Other.Validators.Items;
using OrderBoard.AppServices.Other.Validators.ItemValidator;
using OrderBoard.AppServices.Other.Validators.OrderItems;
using OrderBoard.AppServices.Other.Validators.Orders;
using OrderBoard.AppServices.Other.Validators.Users;
using OrderBoard.ComponentRegistrator;
using OrderBoard.Contracts.UserDto;
using OrderBoard.DataAccess;
using Serilog;
using System.Text;

namespace OrderBoard.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Academy API", Version = "v1" });

                var docTypeMarkers = new[]
                {
                    typeof(UserInfoModel),
                    typeof(UserController)
                };

                foreach (var marker in docTypeMarkers)
                {
                    var xmlFile = $"{marker.Assembly.GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                    if (File.Exists(xmlPath))
                    {
                        options.IncludeXmlComments(xmlPath);
                    }
                }

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"Jwt Authorization heder using the Bearer scheme.
                                    Enter: 'Bearer'
                                    Example: 'Bearer SecretKey'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme="oauth2",
                            Name="Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<String>()
                    }
                });
            });

            builder.Services.AddApplicationServices();
            builder.Services.AddDbContext<OrderBoardDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("ConnectionString")));
            
            builder.Services.AddMvc();
            builder.Services.AddFluentValidation();

            

            builder.Services.AddFluentValidationAutoValidation();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(option =>
                {
                    var secretKey = builder.Configuration["Jwt:Key"];

                    option.RequireHttpsMetadata = false;
                    option.SaveToken = true;
                    option.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateActor = false,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                    };
                });
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddAuthorization();


            builder.Host.UseSerilog((context, provider, config) =>
            {
                config.ReadFrom.Configuration(context.Configuration)
                    .Enrich.WithEnvironmentName();
            });

            var app = builder.Build();
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}