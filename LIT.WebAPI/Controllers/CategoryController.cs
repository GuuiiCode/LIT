using LIT.Application.Services.Interfaces;
using LIT.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LIT.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryViewModel>>> Get()
        {
            var categories = await _categoryService.GetAllCategories();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryViewModel>> Get(Guid id)
        {
            var category = await _categoryService.GetCategory(id);
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Post(BaseCategoryViewModel categoryViewModel)
        {
            var category = await _categoryService.InsertCategory(categoryViewModel);
            return CreatedAtRoute(new { category.Id }, category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, CategoryViewModel category)
        {
            category.Id = id;
            await _categoryService.UpdateCategory(id, category);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _categoryService.DeleteCategory(id);
            return NoContent();
        }
    }
}
