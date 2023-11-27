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
            var categories = await _categoryRepository.GetAllAsync(cancellationToken);

            return from product in products
                   join category in categories
                   on product.CategoryId equals category.Id
                   select new ProductViewModel
                   {
                       Id = product.Id,
                       Name = product.Name,
                       Price = product.Price,
                       Color = product.Color,
                       Description = product.Description,
                       CategoryId = product?.CategoryId,
                       CategoryName = category?.Name,
                       CategoryDescription = category?.Description,
                   };
        }

        public async Task<ProductViewModel?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var product = await _producRepository.GetAsync(id, cancellationToken);
            if (product == null)
                return null;

            var category = await _categoryRepository.GetAsync(product.CategoryId, cancellationToken);

            return new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Color = product.Color,
                Description = product.Description,
                CategoryId = product.CategoryId,
                CategoryName = category?.Name,
                CategoryDescription = category?.Description,
            };
        }

        public async Task<ProductResultViewModel> InsertAsync(BaseProductViewModel productViewModel, CancellationToken cancellationToken = default)
        {
            var result = await ExistsCategory(productViewModel.CategoryId, cancellationToken);
            if (!result.Success)
                return new ProductResultViewModel { ResultViewModel = result };

            var product = _mapper.Map<Product>(productViewModel);

            await _producRepository.InsertAsync(product, cancellationToken);

            var productView = _mapper.Map<ProductViewModel>(product);

            return new ProductResultViewModel { ProductViewModel = productView, ResultViewModel = result }; ;
        }

        public async Task<ResultViewModel> UpdateAsync(Guid id, ProductViewModel productViewModel, CancellationToken cancellationToken = default)
        {
            var product = await _producRepository.GetAsync(id, cancellationToken);
            var productExists = GetProductIfNotExistReturnError(id, product);
            if (!productExists.Success)
                return productExists;

            var result = await ExistsCategory(productViewModel.CategoryId, cancellationToken);
            if (!result.Success)
                return result;

            product = _mapper.Map<Product>(productViewModel);
            await _producRepository.UpdateAsync(product, cancellationToken);

            return result;
        }

        public async Task<ResultViewModel> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var product = await _producRepository.GetAsync(id, cancellationToken);
            var productExists = GetProductIfNotExistReturnError(id, product);
            if (!productExists.Success)
                return productExists;

            await _producRepository.DeleteAsync(id, cancellationToken);
            return new ResultViewModel { Success = true };
        }

        private async Task<ResultViewModel> ExistsCategory(Guid? id, CancellationToken cancellationToken)
        {
            if (id.HasValue && !await _categoryRepository.Exists(id.Value, cancellationToken))
                return new ResultViewModel { Success = false, Error = $"Category '{id}' not found"};

            return new ResultViewModel { Success = true };
        }

        private ResultViewModel GetProductIfNotExistReturnError(Guid id, Product? product)
        {
            if (product == null)
                return new ResultViewModel { Success = false, Error = $"Product '{id}' not found" };

            return new ResultViewModel { Success = true };
        }
    }
}
