using LIT.Application.ViewModels;

namespace LIT.Application.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryViewModel?> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<CategoryViewModel>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<CategoryViewModel> InsertAsync(BaseCategoryViewModel categoryViewModel, CancellationToken cancellationToken = default);
        Task<ResultViewModel> UpdateAsync(Guid id, CategoryViewModel categoryViewModel, CancellationToken cancellationToken = default);
        Task<ResultViewModel> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
