using LIT.Application.ViewModels;
using LIT.Domain.Entities;

namespace LIT.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductViewModel?> GetProduct(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<ProductViewModel>> GetAllProducts(CancellationToken cancellationToken = default);
        Task<ProductViewModel> InsertProduct(BaseProductViewModel productViewModel, CancellationToken cancellationToken = default);
        Task UpdateProduct(Guid id, ProductViewModel productViewModel, CancellationToken cancellationToken = default);
        Task DeleteProduct(Guid id, CancellationToken cancellationToken = default);
    }
}
