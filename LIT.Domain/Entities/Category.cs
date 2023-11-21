using ControlExpenses.Domain.Entities;

namespace LIT.Domain.Entities
{
    public class Category : BaseEntity
    {
        public Category(string name, 
                        string description)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
    }
}
