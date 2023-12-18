using LIT.Domain.Entities;
using LIT.Domain.Interfaces.Repositories;
using LIT.Infra.Context;
using System.Diagnostics.CodeAnalysis;

namespace LIT.Infra.Repositories
{
    [ExcludeFromCodeCoverage]
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(MongoDBContext context) : base(context)
        {
        }
    }
}
