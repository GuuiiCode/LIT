using LIT.Domain.Interfaces.Repositories;
using LIT.Infra.Context;
using LIT.Infra.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System.Diagnostics.CodeAnalysis;

namespace LIT.Infra.Configuration
{
    [ExcludeFromCodeCoverage]
    public static class RepositoryCollectionExtensions
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
    }
}
