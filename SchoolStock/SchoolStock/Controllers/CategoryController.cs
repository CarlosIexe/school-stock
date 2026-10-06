using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolStock.Data.DTO;
using SchoolStock.Services;


//GET       -> Admin / Gestor / User
//POST      -> Admin / Gestor
//PUT       -> Admin / Gestor
//DELETE    -> Admin
namespace SchoolStock.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }
 
        [HttpGet]
        public IActionResult FindAll()
        {
            return Ok(_service.FindAll());
        }

        [HttpGet("{id}")]
        public IActionResult FindById(long id)
        {
            var category = _service.FindById(id);

            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [Authorize(Policy = "CanManageCategories")]
        [HttpPost]
        public IActionResult Create([FromBody] CategoryDTO category)
        {
            if (category == null)
                return BadRequest();

            var createdCategory = _service.Create(category);

            return Ok(createdCategory);
        }

        [Authorize(Policy = "CanManageCategories")]
        [HttpPut]
        public IActionResult Update([FromBody] CategoryDTO category)
        {
            if (category == null)
                return BadRequest();

            var updatedCategory = _service.Update(category);

            return Ok(updatedCategory);
        }

        [Authorize(Policy = "CanDelete")]
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _service.Delete(id);

            return NoContent();
        }
    }
}
