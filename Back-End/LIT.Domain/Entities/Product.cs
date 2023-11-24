using ControlExpenses.Domain.Entities;
using System.ComponentModel.DataAnnotations;

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

        [Required(ErrorMessage = "Fill in the Name field")]
        [StringLength(100, ErrorMessage = "Maximum 100 characters")]
        public string Name { get; private set; }

        [Required(ErrorMessage = "Fill in the Description field")]
        [StringLength(150, ErrorMessage = "Maximum 150 characters")]
        public string Description { get; private set; }

        [Required(ErrorMessage = "Fill in the Price field")]
        public decimal Price { get; private set; }

        [Required(ErrorMessage = "Fill in the Category field")]
        public Guid CategoryId { get; private set; }

        public string? Color { get; private set; }
    }
}
