﻿using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace LIT.Application.ViewModels
{
    [ExcludeFromCodeCoverage]
    public class CategoryViewModel : BaseCategoryViewModel
    {
        public Guid Id { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class BaseCategoryViewModel
    {
        [Required(ErrorMessage = "Fill in the Name field")]
        [StringLength(100, ErrorMessage = "Maximum 100 characters")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Fill in the Description field")]
        [StringLength(150, ErrorMessage = "Maximum 150 characters")]
        public string? Description { get; set; }
    }

}
