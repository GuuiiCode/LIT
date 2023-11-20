using LIT.Application.Services.Interfaces;
using LIT.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LIT.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductViewModel>> Get()
        {
            var products = _productService.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public ActionResult<ProductViewModel> Get(Guid id)
        {
            var product = _productService.GetAsync(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public IActionResult Post(ProductViewModel product)
        {
            _productService.InsertAsync(product);
            return CreatedAtRoute(new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, ProductViewModel product)
        {
            var existingProduct = _productService.GetAsync(id);
            if (existingProduct == null)
                return NotFound();

            _productService.UpdateAsync(id, product);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var product = _productService.GetAsync(id);
            if (product == null)
                return NotFound();

            _productService.DeleteAsync(id);

            return NoContent();
        }
    }
}
