using ControlExpenses.Domain.Entities;

namespace LIT.Domain.Entities
{
    public class Category : BaseEntity
    {
        public Category(Guid id, 
                        string name, 
                        string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }

        public void Change(string name, 
                           string description)
        {
            Name = name;
            Description = description;
        }
    }
}
