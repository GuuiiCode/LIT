using AutoMapper;
using LIT.Application.Services.Interfaces;
using LIT.Application.ViewModels;
using LIT.Domain.Entities;
using LIT.Domain.Interfaces.Repositories;

namespace LIT.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository,
                               IProductRepository productRepository,
                               IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var categories = await _categoryRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<CategoryViewModel>>(categories);
        }

        public async Task<CategoryViewModel?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var category = await GetCategoryIfNotExistReturnNull(id, cancellationToken);
            if (category == null)
                return null;

            return _mapper.Map<CategoryViewModel>(category);
        }

        public async Task<CategoryViewModel> InsertAsync(BaseCategoryViewModel categoryViewModel, CancellationToken cancellationToken = default)
        {
            var category = _mapper.Map<Category>(categoryViewModel);
            await _categoryRepository.InsertAsync(category, cancellationToken);
            return _mapper.Map<CategoryViewModel>(category);
        }

        public async Task<ResultViewModel> UpdateAsync(Guid id, CategoryViewModel categoryViewModel, CancellationToken cancellationToken = default)
        {
            var category = await GetCategoryIfNotExistReturnNull(id, cancellationToken);
            if (category == null)
                return new ResultViewModel { Success = false, Error = $"Category '{id}' not found" };

            category = _mapper.Map<Category>(categoryViewModel);
            await _categoryRepository.UpdateAsync(category, cancellationToken);

            return new ResultViewModel { Success = true };
        }

        public async Task<ResultViewModel> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var category = await GetCategoryIfNotExistReturnNull(id, cancellationToken);
            if (category == null)
                return new ResultViewModel { Success = false, Error = $"Category '{id}' not found" };

            var result = await CheckIfExistCategoryInAnyProduct(id, cancellationToken);
            if (!result.Success)
                return result;

            await _categoryRepository.DeleteAsync(id, cancellationToken);
            return result;
        }

        private async Task<ResultViewModel> CheckIfExistCategoryInAnyProduct(Guid id, CancellationToken cancellationToken)
        {
            var hasProduct = await _productRepository.Find(x => x.CategoryId == id, cancellationToken);
            if (hasProduct)
                return new ResultViewModel { Success = false, Error = $"You can't delete this Category beacuse it being used in one or more Products" };
            
            return new ResultViewModel { Success = true };
        }

        private async Task<Category?> GetCategoryIfNotExistReturnNull(Guid id, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetAsync(id, cancellationToken);
            if (category == null)
                return null;

            return await _categoryRepository.GetAsync(id, cancellationToken);
        }
    }
}
