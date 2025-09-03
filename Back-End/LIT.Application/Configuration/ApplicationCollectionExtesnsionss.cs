using LIT.Application.AutoMapper;
using LIT.Application.Services;
using LIT.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace LIT.Application.Configuration
{
    [ExcludeFromCodeCoverage]
    public static class ApplicationCollectionExtesnsionss
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IUserService, UserService>();
            return services;
        }

        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperSetup));
            return services;
        }
    }
}
