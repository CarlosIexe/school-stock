using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolStock.Data.DTO;
using SchoolStock.Services;

namespace SchoolStock.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SchoolController : ControllerBase
    {
        private readonly ISchoolService _service;

        public SchoolController(ISchoolService service)
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
            var school = _service.FindById(id);

            if (school == null)
                return NotFound();

            return Ok(school);
        }

        [HttpPost]
        [Authorize(Policy="CanManageSchools")]
        public IActionResult Create([FromBody] SchoolDTO school)
        {
            if (school == null)
                return BadRequest();

            var createdSchool = _service.Create(school);

            return Ok(createdSchool);
        }

        [HttpPut]
        [Authorize(Policy ="CanManageSchools")]
        public IActionResult Update([FromBody] SchoolDTO school)
        {
            if (school == null)
                return BadRequest();

            var updatedSchool = _service.Update(school);

            return Ok(updatedSchool);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles ="CanDelete")]
        public IActionResult Delete(long id)
        {
            _service.Delete(id);

            return NoContent();
        }
    }
}
