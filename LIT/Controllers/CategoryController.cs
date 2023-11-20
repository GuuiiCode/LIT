using LIT.Application.Services.Interfaces;
using LIT.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LIT.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("{id}")]
        public ActionResult<CategoryViewModel> Get(Guid id)
        {
            var category = _categoryService.GetAsync(id);
            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost]
        public IActionResult Post(CategoryViewModel category)
        {
            _categoryService.InsertAsync(category);
            return CreatedAtRoute(new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, CategoryViewModel category)
        {
            var existingCategory = _categoryService.GetAsync(id);
            if (existingCategory == null)
                return NotFound();

            _categoryService.UpdateAsync(id, category);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var Category = _categoryService.GetAsync(id);
            if (Category == null)
                return NotFound();

            _categoryService.DeleteAsync(id);

            return NoContent();
        }
    }
}
