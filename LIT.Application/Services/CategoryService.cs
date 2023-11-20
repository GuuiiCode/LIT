using LIT.Application.Services.Interfaces;
using LIT.Application.ViewModels;
using LIT.Domain.Entities;
using LIT.Domain.Interfaces.Repositories;

namespace LIT.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var categories = await _categoryRepository.GetAllAsync(cancellationToken);
            return categories.Select(s => new CategoryViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
            });
        }

        public async Task<CategoryViewModel?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var categories = await _categoryRepository.GetAsync(id, cancellationToken);
            if (categories == null)
                return null;

            return new CategoryViewModel
            {
                Id = categories.Id,
                Name = categories.Name,
                Description = categories.Description
            };
        }

        public async Task InsertAsync(CategoryViewModel categoryViewModel, CancellationToken cancellationToken = default)
        {
            var categories = new Category(categoryViewModel.Id,
                                          categoryViewModel.Name,
                                          categoryViewModel.Description);

            await _categoryRepository.InsertAsync(categories, cancellationToken);
        }

        public async Task UpdateAsync(Guid id, CategoryViewModel categoryViewModel, CancellationToken cancellationToken = default)
        {
            var category = await _categoryRepository.GetAsync(id, cancellationToken)
                ?? throw new Exception($"Category '{id}' not found");

            category.Change(categoryViewModel.Name, categoryViewModel.Description);

            await _categoryRepository.UpdateAsync(category, cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var product = await _categoryRepository.GetAsync(id, cancellationToken);
            if (product == null)
                return;

            await _categoryRepository.DeleteAsync(id, cancellationToken);
        }
    }
}
