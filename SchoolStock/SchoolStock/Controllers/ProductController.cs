using Microsoft.AspNetCore.Mvc;
using SchoolStock.Data.DTO;
using SchoolStock.Services;

namespace SchoolStock.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productServices;

        public ProductsController(IProductService service)
        {
            _productServices = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_productServices.FindAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var product = _productServices.FindById(id);

            if (product is null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public IActionResult Post([FromBody] ProductDTO product)
        {
            var createdProduct = _productServices.Create(product);
            if (createdProduct == null) return NotFound();
            return Ok(createdProduct);
        }

        [HttpPut]
        public IActionResult Put([FromBody] ProductDTO product)
        {
            var createdProduct = _productServices.Update(product);
            if (createdProduct == null) return NotFound();
            return Ok(createdProduct);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            _productServices.Delete(id);
            return NoContent();
        }
    }
}
