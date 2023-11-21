using LIT.Application.Services;
using LIT.Application.Services.Interfaces;
using LIT.Domain.Interfaces.Repositories;
using LIT.Infra.Context;
using LIT.Infra.Repositories;
using MongoDB.Driver;

namespace LIT.WebAPI.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMongoDB(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration["MongoDBSettings:ConnectionString"];
            string databaseName = configuration["MongoDBSettings:DatabaseName"];

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);

            services.AddSingleton(new MongoDBContext(database));

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ICategoryService, CategoryService>();

            return services;
        }
    }
}
