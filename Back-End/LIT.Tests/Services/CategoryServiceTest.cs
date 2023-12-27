using AutoMapper;
using LIT.Application.Services;
using LIT.Application.ViewModels;
using LIT.Domain.Entities;
using LIT.Domain.Interfaces.Repositories;
using Moq;
using System.Linq.Expressions;
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
        public async Task InsertAsync()
        {
            _mapper.Setup(x => x.Map<Category>(It.IsAny<BaseCategoryViewModel>()))
                .Returns(CreateCategoryObject());

            _mapper.Setup(x => x.Map<BaseCategoryViewModel>(It.IsAny<Category>()))
                .Returns(CreateCategoryViewModelObject());

            var result = await _categoryService.InsertAsync(CreateBaseCategoryObject(), It.IsAny<CancellationToken>());

            Assert.NotNull(result);
            Assert.Equal("name CategoryView", result.Name);
            Assert.Equal("description CategoryView", result.Description);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            _categoryRepository.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(CreateCategoryObject());

            _mapper.Setup(x => x.Map<Category>(It.IsAny<BaseCategoryViewModel>()))
                .Returns(CreateCategoryObject());

            var result = await _categoryService.UpdateAsync(It.IsAny<Guid>(), CreateCategoryViewModelObject(), It.IsAny<CancellationToken>());

            Assert.True(result.Success);
            Assert.Null(result.Error);
        }

        [Fact]
        public async Task UpdateAsync_WhenCategoryIsNull_ReturnError()
        {
            _categoryRepository.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()));

            var result = await _categoryService.UpdateAsync(It.IsAny<Guid>(), CreateCategoryViewModelObject(), It.IsAny<CancellationToken>());

            Assert.False(result.Success);
            Assert.Equal("Category '00000000-0000-0000-0000-000000000000' not found", result.Error);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            _categoryRepository.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(CreateCategoryObject());

            _productRepository.Setup(x => x.Find(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            var result = await _categoryService.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>());

            Assert.True(result.Success);
            Assert.Null(result.Error);
        }

        [Fact]
        public async Task DeleteAsync_WhenCategoryIsNull_ReturnError()
        {
            _categoryRepository.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()));

            var result = await _categoryService.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>());

            Assert.False(result.Success);
            Assert.Equal("Category '00000000-0000-0000-0000-000000000000' not found", result.Error);
        }

        [Fact]
        public async Task DeleteAsync_IfExistCategoryInAnyProduct_ReturnError()
        {
            _categoryRepository.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(CreateCategoryObject());

            _productRepository.Setup(x => x.Find(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var result = await _categoryService.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>());

            Assert.False(result.Success);
            Assert.Equal("You can't delete this Category beacuse it being used in one or more Products", result.Error);
        }

        [Fact]
        public async Task GetAllAsync()
        {
            _categoryRepository.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(CreateCategoriesObject());

            _mapper.Setup(x => x.Map<IEnumerable<CategoryViewModel>>(It.IsAny<IEnumerable<Category>>()))
                .Returns(CreateCategoriesViewModelObject());

            var result = await _categoryService.GetAllAsync(It.IsAny<CancellationToken>());

            Assert.NotNull(result);
            Assert.Equal("name CategoryView", result.ToList()[0].Name);
            Assert.Equal("description CategoryView", result.ToList()[0].Description);
            Assert.Equal("name CategoryView 2", result.ToList()[1].Name);
            Assert.Equal("description CategoryView 2", result.ToList()[1].Description);
        }

        [Fact]
        public async Task GetAsync()
        {
            _categoryRepository.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(CreateCategoryObject());

            _mapper.Setup(x => x.Map<CategoryViewModel>(It.IsAny<Category>()))
                .Returns(CreateCategoryViewModelObject());

            var result = await _categoryService.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>());

            Assert.NotNull(result);
            Assert.Equal("name CategoryView", result.Name);
            Assert.Equal("description CategoryView", result.Description);
        }

        [Fact]
        public async Task GetAsync_WhenCategoryIdIsNull_ReturnNull()
        {
            _categoryRepository.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()));

            var result = await _categoryService.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>());

            Assert.Null(result);
        }

        public Category CreateCategoryObject()
        {
            return new Category("name Category", "description Category");
        }

        public IEnumerable<Category> CreateCategoriesObject()
        {
            return new List<Category>()
            {
                new("name Category", "description Category"),
                new("name Category 2", "description Category 2")
            };
        }

        public CategoryViewModel CreateCategoryViewModelObject()
        {
            return new CategoryViewModel
            {
                Id = Guid.NewGuid(),
                Name = "name CategoryView",
                Description = "description CategoryView"
            };
        }

        public IEnumerable<CategoryViewModel> CreateCategoriesViewModelObject()
        {
            return new List<CategoryViewModel>
            {
                new() {
                    Id = Guid.NewGuid(),
                    Name = "name CategoryView",
                    Description = "description CategoryView"
                },
                new() {
                    Id = Guid.NewGuid(),
                    Name = "name CategoryView 2",
                    Description = "description CategoryView 2"
                }
            };
        }

        public BaseCategoryViewModel CreateBaseCategoryObject()
        {
            return new BaseCategoryViewModel
            {
                Name = "name BaseCategoryViewModel",
                Description = "description BaseCategoryViewModel",
            };
        }
    }
}
