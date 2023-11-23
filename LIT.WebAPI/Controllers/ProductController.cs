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
            var products = await _productService.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductViewModel>> Get(Guid id)
        {
            var product = await _productService.GetProduct(id);
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Post(BaseProductViewModel productViewModel)
        {
            var product = await _productService.InsertProduct(productViewModel);
            return CreatedAtRoute(new { product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, ProductViewModel product)
        {
            product.Id = id;
            await _productService.UpdateProduct(id, product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _productService.DeleteProduct(id);
            return NoContent();
        }
    }
}
