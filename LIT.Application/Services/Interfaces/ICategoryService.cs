using LIT.Application.ViewModels;

namespace LIT.Application.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryViewModel?> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<CategoryViewModel>> GetAllAsync(CancellationToken cancellationToken = default);
        Task InsertAsync(CategoryViewModel categoryViewModel, CancellationToken cancellationToken = default);
        Task UpdateAsync(Guid id, CategoryViewModel categoryViewModel, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
