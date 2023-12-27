using LIT.Application.Services.Interfaces;
using LIT.WebAPI.Controllers;
using Moq;

namespace LIT.Tests.Controllers
{
    public class CategoryControllerTest
    {
        private readonly Mock<ICategoryService> _categoryService;
        private readonly CategoryController _categoryController;

        public CategoryControllerTest()
        {
            _categoryService = new Mock<ICategoryService>();
            _categoryController = new CategoryController(_categoryService.Object);
        }
    }
}
