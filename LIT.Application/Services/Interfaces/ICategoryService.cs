using LIT.Application.ViewModels;

namespace LIT.Application.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryViewModel?> GetCategory(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<CategoryViewModel>> GetAllCategories(CancellationToken cancellationToken = default);
        Task<CategoryViewModel> InsertCategory(BaseCategoryViewModel categoryViewModel, CancellationToken cancellationToken = default);
        Task UpdateCategory(Guid id, CategoryViewModel categoryViewModel, CancellationToken cancellationToken = default);
        Task DeleteCategory(Guid id, CancellationToken cancellationToken = default);
    }
}
