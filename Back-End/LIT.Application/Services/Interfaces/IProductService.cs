using LIT.Application.ViewModels;

namespace LIT.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductViewModel?> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<ProductViewModel>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<ProductResultViewModel> InsertAsync(BaseProductViewModel productViewModel, CancellationToken cancellationToken = default);
        Task<ResultViewModel> UpdateAsync(Guid id, ProductViewModel productViewModel, CancellationToken cancellationToken = default);
        Task<ResultViewModel> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
