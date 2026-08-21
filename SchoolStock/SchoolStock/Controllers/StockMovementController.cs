namespace SchoolStock.Controllers
{
    using global::SchoolStock.Services;
    using Microsoft.AspNetCore.Mvc;

    namespace SchoolStock.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class StockMovementController : ControllerBase
        {
            private readonly IStockMovementService _service;

            public StockMovementController(
                IStockMovementService service)
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
                try
                {
                    var movement =
                        _service.FindById(id);

                    return Ok(movement);
                }
                catch (Exception ex)
                {
                    return NotFound(new
                    {
                        message = ex.Message
                    });
                }
            }
        }
    }
}
