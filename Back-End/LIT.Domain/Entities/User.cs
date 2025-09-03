using ControlExpenses.Domain.Entities;
using LIT.Domain.ValueObjects;

namespace LIT.Domain.Entities
{
    public class User : Entity
    {
        protected User() {}

        public User(UserName userName,  Password password)
        {
            Id = Guid.NewGuid();
            UserName = userName;
            Password = password;
        }

        public UserName UserName { get; private set; }
        public Password Password { get; private set; }

        public bool VerifyPassword(string plainPassword) => Password.Verify(plainPassword);
    }
}
