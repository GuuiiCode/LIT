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

        public ProductService(IProductRepository producRepository, ICategoryRepository categoryRepository)
        {
            _producRepository = producRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var products = await _producRepository.GetAllAsync(cancellationToken);
            return products.Select(s => new ProductViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                Price = s.Price,
                Color = s.Color,
                CategoryId = s.CategoryId,
            });
        }

        public async Task<ProductViewModel?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var product = await _producRepository.GetAsync(id, cancellationToken);

            if (product == null)
                return null;

            return new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Color = product.Color,
                CategoryId = product.CategoryId,
            };
        }

        public async Task InsertAsync(ProductViewModel productViewModel, CancellationToken cancellationToken = default)
        {
            var category = await _categoryRepository.GetAsync(productViewModel.CategoryId, cancellationToken)
                ?? throw new Exception($"Category '{productViewModel.CategoryId}' not found");

            var product = new Product(productViewModel.Name,
                                      productViewModel.Description,
                                      productViewModel.Price,
                                      productViewModel.Color,
                                      category.Id);

            await _producRepository.InsertAsync(product, cancellationToken);
        }

        public async Task UpdateAsync(Guid id, ProductViewModel productViewModel, CancellationToken cancellationToken = default)
        {
            var product = await _producRepository.GetAsync(productViewModel.Id, cancellationToken)
                ?? throw new Exception($"Product '{productViewModel.Id}' not found");

            var category = await _categoryRepository.GetAsync(productViewModel.CategoryId, cancellationToken)
                ?? throw new Exception($"Category '{productViewModel.CategoryId}' not found");

            product.Change(productViewModel.Name,
                           productViewModel.Description,
                           productViewModel.Price,
                           productViewModel.Color,
                           category.Id);

            await _producRepository.UpdateAsync(product, cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var product = await _producRepository.GetAsync(id, cancellationToken);
            if (product == null)
                return;

            await _producRepository.DeleteAsync(id, cancellationToken);
        }
    }
}
