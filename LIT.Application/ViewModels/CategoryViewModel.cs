using System.ComponentModel.DataAnnotations;

namespace LIT.Application.ViewModels
{
    public class CategoryViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Fill in the Name field")]
        [StringLength(100, ErrorMessage = "Maximum 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Fill in the Description field")]
        [StringLength(150, ErrorMessage = "Maximum 150 characters")]
        public string Description { get; set; }
    }
}
