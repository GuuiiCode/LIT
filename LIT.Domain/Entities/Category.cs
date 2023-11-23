using ControlExpenses.Domain.Entities;
using System.ComponentModel.DataAnnotations;

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
        }

        [Required(ErrorMessage = "Fill in the Name field")]
        [StringLength(100, ErrorMessage = "Maximum 100 characters")]
        public string Name { get; private set; }

        [Required(ErrorMessage = "Fill in the Description field")]
        [StringLength(150, ErrorMessage = "Maximum 150 characters")]
        public string Description { get; private set; }
    }
}
