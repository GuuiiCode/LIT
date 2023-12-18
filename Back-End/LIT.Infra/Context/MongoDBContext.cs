using MongoDB.Driver;
using System.Diagnostics.CodeAnalysis;

namespace LIT.Infra.Context
{
    [ExcludeFromCodeCoverage]
    public class MongoDBContext
    {
        private readonly IMongoDatabase _database;

        public MongoDBContext(IMongoDatabase database)
        {
            _database = database;
        }

        public IMongoCollection<TEntity> GetCollection<TEntity>()
        {
            return _database.GetCollection<TEntity>(typeof(TEntity).Name);
        }
    }
}
