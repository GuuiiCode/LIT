using System.Diagnostics.CodeAnalysis;

namespace LIT.Application.ViewModels
{
    [ExcludeFromCodeCoverage]
    public class ResultViewModel
    {
        public bool Success { get; set; }
        public string? Error { get; set; }
    }
}
