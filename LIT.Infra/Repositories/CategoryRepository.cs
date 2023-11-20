using LIT.Domain.Entities;
using LIT.Domain.Interfaces.Repositories;
using LIT.Infra.Context;

namespace LIT.Infra.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(MongoDBContext context) : base(context)
        {
        }
    }
}
