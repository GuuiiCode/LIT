using LIT.Application.Services.Interfaces;
using LIT.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LIT.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> Get()
        {
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductViewModel>> Get(Guid id)
        {
            var product = await _productService.GetAsync(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Post(BaseProductViewModel productViewModel)
        {
            var result = await _productService.InsertAsync(productViewModel);

            if (!result.ResultViewModel.Success)
                return NotFound(result.ResultViewModel.Error);

            return CreatedAtRoute(new { result.ProductViewModel.Id }, result.ProductViewModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, ProductViewModel product)
        {
            product.Id = id;
            var result = await _productService.UpdateAsync(id, product);

            if (!result.Success)
                return BadRequest(result.Error);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _productService.DeleteAsync(id);
            if (!result.Success)
                return NotFound(result.Error);

            return NoContent();
        }
    }
}
