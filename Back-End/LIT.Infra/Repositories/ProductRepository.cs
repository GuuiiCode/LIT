using LIT.Domain.Entities;
using LIT.Domain.Interfaces.Repositories;
using LIT.Infra.Context;

namespace LIT.Infra.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(MongoDBContext context) : base(context)
        {
        }
    }
}
