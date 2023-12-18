using LIT.Domain.Entities;
using LIT.Domain.Interfaces.Repositories;
using LIT.Infra.Context;
using System.Diagnostics.CodeAnalysis;

namespace LIT.Infra.Repositories
{
    [ExcludeFromCodeCoverage]
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(MongoDBContext context) : base(context)
        {
        }
    }
}
