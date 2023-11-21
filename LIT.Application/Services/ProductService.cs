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
            //TO DO - Create an method for the repository to validate if category exists
            var product = await _producRepository.GetAsync(id, cancellationToken);
            if (product == null)
                return null;

            return _mapper.Map<ProductViewModel>(product);
        }

        public async Task InsertAsync(ProductViewModel productViewModel, CancellationToken cancellationToken = default)
        {
            //TO DO - Create an method for the repository to validate if category exists
            var category = await _categoryRepository.GetAsync(productViewModel.CategoryId, cancellationToken)
                ?? throw new Exception($"Category '{productViewModel.CategoryId}' not found");

            var product = _mapper.Map<Product>(productViewModel);

            await _producRepository.InsertAsync(product, cancellationToken);
        }

        public async Task UpdateAsync(Guid id, ProductViewModel productViewModel, CancellationToken cancellationToken = default)
        {
            //TO DO - Create an method for the repository to validate if category exists
            var product = await _producRepository.GetAsync(productViewModel.Id, cancellationToken)
                ?? throw new Exception($"Product '{productViewModel.Id}' not found");

            var category = await _categoryRepository.GetAsync(productViewModel.CategoryId, cancellationToken)
                ?? throw new Exception($"Category '{productViewModel.CategoryId}' not found");

            product = _mapper.Map<Product>(productViewModel);

            await _producRepository.UpdateAsync(product, cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            //TO DO - Create an method for the repository to validate if category exists
            var product = await _producRepository.GetAsync(id, cancellationToken);
            if (product == null)
                return;

            await _producRepository.DeleteAsync(id, cancellationToken);
        }
    }
}
