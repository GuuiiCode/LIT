using ControlExpenses.Domain.Entities;

namespace LIT.Domain.Entities
{
    public class Category : Entity
    {
        public Category(string name, 
                        string description)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            ValidateFields();
        }

        public string Name { get; private set; }
        public string Description { get; private set; }

        public void ValidateFields()
        {
            NameIsValid();
            DescriptionIsValid();
        }

        public void NameIsValid()
        { 
            if(string.IsNullOrEmpty(Name))
                throw new ArgumentNullException(nameof(Name));

            if (Name.Length > 100)
                throw new ArgumentOutOfRangeException($"{nameof(Name)} Maximum 100 characters");
        }

        public void DescriptionIsValid()
        {
            if (string.IsNullOrEmpty(Description))
                throw new ArgumentNullException(nameof(Description));

            if (Description.Length > 150)
                throw new ArgumentOutOfRangeException($"{nameof(Description)} Maximum 150 characters");
        }
    }
}
