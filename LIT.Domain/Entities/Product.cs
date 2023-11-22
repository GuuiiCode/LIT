using ControlExpenses.Domain.Entities;
using System.Net;

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
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public string? Color { get; private set; }
        public Guid CategoryId { get; private set; }
    }
}
