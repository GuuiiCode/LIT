using ControlExpenses.Domain.Entities;

namespace LIT.Domain.Entities
{
    public class Product : BaseEntity
    {
        public Product(Guid id,
                       string name, 
                       string description, 
                       decimal price, 
                       string color,
                       Guid categoryId)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Color = color;
            CategoryId = categoryId;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public string Color { get; private set; }
        public Guid CategoryId { get; private set; }

        public void Change(string name,
                           string description,
                           decimal price,
                           string color,
                           Guid categoryId)
        {
            Name = name;
            Description = description;
            Price = price;
            Color = color;
            CategoryId = categoryId;
        }
    }
}
