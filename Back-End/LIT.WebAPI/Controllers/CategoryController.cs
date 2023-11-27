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
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryViewModel>> Get(Guid id)
        {
            var category = await _categoryService.GetAsync(id);
            if(category == null) 
                return NotFound();

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BaseCategoryViewModel categoryViewModel)
        {
            var category = await _categoryService.InsertAsync(categoryViewModel);
            if(category == null)
                return BadRequest();

            return CreatedAtRoute(new { category.Id }, category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, CategoryViewModel category)
        {
            category.Id = id;
            var result = await _categoryService.UpdateAsync(id, category);

            if(!result.Success)
                return BadRequest(result.Error);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _categoryService.DeleteAsync(id);
            if(!result.Success)
                return BadRequest(result.Error);

            return Ok();
        }
    }
}
