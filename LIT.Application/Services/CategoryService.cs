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
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var categories = await _categoryRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<CategoryViewModel>>(categories);
        }

        public async Task<CategoryViewModel?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            //TO DO - Create an method for the repository to validate if category exists
            var category = await _categoryRepository.GetAsync(id, cancellationToken);
            if (category == null) 
                return null;
            return _mapper.Map<CategoryViewModel>(category);
        }

        public async Task InsertAsync(CategoryViewModel categoryViewModel, CancellationToken cancellationToken = default)
        {
            var category = _mapper.Map<Category>(categoryViewModel);
            await _categoryRepository.InsertAsync(category, cancellationToken);
        }

        public async Task UpdateAsync(Guid id, CategoryViewModel categoryViewModel, CancellationToken cancellationToken = default)
        {
            //TO DO - Create an method for the repository to validate if category exists
            var category = await _categoryRepository.GetAsync(id, cancellationToken)
                ?? throw new Exception($"Category '{id}' not found");

            category = _mapper.Map<Category>(categoryViewModel);

            await _categoryRepository.UpdateAsync(category, cancellationToken);
        }

        //TO DO - Create validation to not let delete if there is register in product
        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            //TO DO - Create an method for the repository to validate if category exists
            var category = await _categoryRepository.GetAsync(id, cancellationToken);
            if (category == null)
                return;
            await _categoryRepository.DeleteAsync(id, cancellationToken);
        }
    }
}
