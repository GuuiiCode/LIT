using ControlExpenses.Domain.Entities;

namespace LIT.Domain.Entities
{
    public class Product : Entity
    {
        public Product(string name, 
                       string description, 
                       decimal price, 
                       string? color,
                       Guid categoryId)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Price = price;
            Color = color;
            CategoryId = categoryId;
            ValidateFields();
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public Guid CategoryId { get; private set; }
        public string? Color { get; private set; }

        public void ValidateFields()
        {
            NameIsValid();
            DescriptionIsValid();
            PriceIsValid();
            CategoryIdIsValid();
        }

        public void NameIsValid()
        {
            if (string.IsNullOrEmpty(Name))
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

        public void PriceIsValid()
        {
            if (Price.Equals(null))
                throw new ArgumentNullException(nameof(Price));
        }

        public void CategoryIdIsValid()
        {
            if (CategoryId == Guid.Empty)
                throw new ArgumentNullException(nameof(CategoryId));
        }
    }
}
