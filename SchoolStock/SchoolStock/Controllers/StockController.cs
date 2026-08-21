using Microsoft.AspNetCore.Mvc;
using SchoolStock.Services;

namespace SchoolStock.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockController : ControllerBase
    {
        private readonly IStockService _service;

        public StockController(IStockService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult FindAll()
        {
            return Ok(_service.FindAll());
        }

        [HttpGet("product/{productId}")]
        public IActionResult Find(
            long productId,
            [FromQuery] long? schoolId)
        {
            try
            {
                var stock = _service.Find(
                    productId,
                    schoolId);

                return Ok(stock);
            }
            catch (Exception ex)
            {
                return NotFound(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpPost("entry")]
        public IActionResult Add(
            [FromQuery] long productId,
            [FromQuery] long? schoolId,
            [FromQuery] int quantity)
        {
            try
            {
                var stock = _service.Add(
                    productId,
                    schoolId,
                    quantity);

                return Ok(stock);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpPost("remove")]
        public IActionResult Remove(
            [FromQuery] long productId,
            [FromQuery] long? schoolId,
            [FromQuery] int quantity)
        {
            try
            {
                var stock = _service.Remove(
                    productId,
                    schoolId,
                    quantity);

                return Ok(stock);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpPost("transfer")]
        public IActionResult Transfer(
            [FromQuery] long productId,
            [FromQuery] long? originSchoolId,
            [FromQuery] long? destinationSchoolId,
            [FromQuery] int quantity)
        {
            try
            {
                _service.Transfer(
                    productId,
                    originSchoolId,
                    destinationSchoolId,
                    quantity);

                return Ok(new
                {
                    message = "Transferência realizada com sucesso."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }
    }
}
