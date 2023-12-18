using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace LIT.Application.ViewModels
{
    [ExcludeFromCodeCoverage]
    public class ProductViewModel : BaseProductViewModel
    {
        public Guid Id { get; set; }
        public string? CategoryName { get; set; }
        public string? CategoryDescription { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class ProductResultViewModel
    {
        public ProductViewModel ProductViewModel { get; set; }
        public ResultViewModel ResultViewModel { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class BaseProductViewModel
    {
        [Required(ErrorMessage = "Fill in the Name field")]
        [StringLength(100, ErrorMessage = "Maximum 100 characters")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Fill in the Description field")]
        [StringLength(150, ErrorMessage = "Maximum 150 characters")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Fill in the Price field")]
        public decimal? Price { get; set; }

        [Required(ErrorMessage = "Fill in the Category field")]
        public Guid? CategoryId { get; set; }

        public string? Color { get; set; }
    }
}
