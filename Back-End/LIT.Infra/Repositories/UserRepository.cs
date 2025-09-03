using LIT.Domain.Entities;
using LIT.Domain.Interfaces.Repositories;
using LIT.Infra.Context;
using MongoDB.Driver;

namespace LIT.Infra.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository(MongoDBContext context) : base(context)
        {
            _users = context.GetCollection<User>();
        }

        public async Task<User?> GetByUserNameAsync(string userName)
            => await _users.Find(user => user.UserName.Value ==  userName).FirstOrDefaultAsync();
    }
}
