using AutoMapper;
using LIT.Application.Services.Interfaces;
using LIT.Application.ViewModels;
using LIT.Domain.Entities;
using LIT.Domain.Interfaces.Repositories;

namespace LIT.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _producRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository producRepository, 
                              ICategoryRepository categoryRepository, 
                              IMapper mapper)
        {
            _producRepository = producRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var products = await _producRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<ProductViewModel>>(products);
        }

        public async Task<ProductViewModel?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var product = await GetProductIfNotExistsThrowException(id, cancellationToken);
            return _mapper.Map<ProductViewModel>(product);
        }

        public async Task<ProductViewModel> InsertAsync(BaseProductViewModel productViewModel, CancellationToken cancellationToken = default)
        {
            await ExistsCategory(productViewModel.CategoryId, cancellationToken);
            var product = _mapper.Map<Product>(productViewModel);
            await _producRepository.InsertAsync(product, cancellationToken);
            return _mapper.Map<ProductViewModel>(product);
        }

        public async Task UpdateAsync(Guid id, ProductViewModel productViewModel, CancellationToken cancellationToken = default)
        {
            var product = await GetProductIfNotExistsThrowException(id, cancellationToken);
            await ExistsCategory(productViewModel.CategoryId, cancellationToken);
            product = _mapper.Map<Product>(productViewModel);
            await _producRepository.UpdateAsync(product, cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await GetProductIfNotExistsThrowException(id, cancellationToken);
            await _producRepository.DeleteAsync(id, cancellationToken);
        }

        private async Task ExistsCategory(Guid? id, CancellationToken cancellationToken)
        {
            if (id.HasValue && !await _categoryRepository.Exists(id.Value, cancellationToken))
                throw new Exception($"Category '{id}' not found");
        }

        private async Task<Product> GetProductIfNotExistsThrowException(Guid id, CancellationToken cancellationToken)
        {
            return await _producRepository.GetAsync(id, cancellationToken) ?? throw new Exception($"Product '{id}' not found");
        }
    }
}
