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

        public async Task<IEnumerable<CategoryViewModel>> GetAllCategories(CancellationToken cancellationToken = default)
        {
            var categories = await _categoryRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<CategoryViewModel>>(categories);
        }

        public async Task<CategoryViewModel?> GetCategory(Guid id, CancellationToken cancellationToken = default)
        {
            var category = await GetCategoryIfNotExistThrowException(id, cancellationToken);
            return _mapper.Map<CategoryViewModel>(category);
        }

        public async Task<CategoryViewModel> InsertCategory(BaseCategoryViewModel categoryViewModel, CancellationToken cancellationToken = default)
        {
            var category = _mapper.Map<Category>(categoryViewModel);
            await _categoryRepository.InsertAsync(category, cancellationToken);
            return _mapper.Map<CategoryViewModel>(category);
        }

        public async Task UpdateCategory(Guid id, CategoryViewModel categoryViewModel, CancellationToken cancellationToken = default)
        {
            var category = await GetCategoryIfNotExistThrowException(id, cancellationToken);
            category = _mapper.Map<Category>(categoryViewModel);
            await _categoryRepository.UpdateAsync(category, cancellationToken);
        }

        public async Task DeleteCategory(Guid id, CancellationToken cancellationToken = default)
        {
            await GetCategoryIfNotExistThrowException(id, cancellationToken);
            await CheckIfExistCategoryInAnyProduct(id, cancellationToken);
            await _categoryRepository.DeleteAsync(id, cancellationToken);
        }

        private async Task CheckIfExistCategoryInAnyProduct(Guid id, CancellationToken cancellationToken)
        {
            var hasProduct = await _productRepository.Find(x => x.CategoryId == id, cancellationToken);
            if (hasProduct)
                throw new Exception($"You can't delete this Category beacuse it being used in one or more Products");
        }

        private async Task<Category> GetCategoryIfNotExistThrowException(Guid id, CancellationToken cancellationToken)
        {
            return await _categoryRepository.GetAsync(id, cancellationToken) ?? throw new Exception($"Category '{id}' not found");
        }
    }
}
