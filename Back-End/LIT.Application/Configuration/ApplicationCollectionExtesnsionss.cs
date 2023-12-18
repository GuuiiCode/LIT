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
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ICategoryService, CategoryService>();
            return services;
        }

        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperSetup));
            return services;
        }
    }
}
