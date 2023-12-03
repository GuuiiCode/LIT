using AutoMapper;
using LIT.Application.Services;
using LIT.Application.ViewModels;
using LIT.Domain.Entities;
using LIT.Domain.Interfaces.Repositories;
using Moq;
using Xunit;

namespace LIT.Tests.Services
{
    public class CategoryServiceTest
    {
        private readonly Mock<ICategoryRepository> _categoryRepository;
        private readonly Mock<IProductRepository> _productRepository;
        private readonly Mock<IMapper> _mapper;
        private readonly CategoryService _categoryService;

        public CategoryServiceTest()
        {
            _categoryRepository = new Mock<ICategoryRepository>();
            _productRepository = new Mock<IProductRepository>();
            _mapper = new Mock<IMapper>();
            _categoryService = new CategoryService(_categoryRepository.Object, 
                                                   _productRepository.Object, 
                                                   _mapper.Object);
        }

        [Fact]
        public async Task InsertAsyncFields()
        {
            _mapper.Setup(x => x.Map<Category>(It.IsAny<BaseCategoryViewModel>()))
                .Returns(CreateCategoryObject());

            var result = await _categoryService.InsertAsync(CreateBaseCategoryObject(), It.IsAny<CancellationToken>());

        }

        public Category CreateCategoryObject()
        {
            return new Category("name", "description");
        }

        public BaseCategoryViewModel CreateBaseCategoryObject()
        {
            return new BaseCategoryViewModel
            {
                Name = "name",
                Description = "description",
            };
        }
    }
}
