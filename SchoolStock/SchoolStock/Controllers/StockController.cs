using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolStock.Data.DTO.Stock;
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
        [Authorize(Policy = "CanManageStock")]
        public IActionResult Add([FromBody] StockEntryRequest request)
        {
            try
            {
                var stock = _service.Add(
                    request.ProductId,
                    request.SchoolId,
                    request.Quantity);

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
        [Authorize(Policy = "CanManageStock")]
        public IActionResult Remove([FromBody] StockRemoveRequest request)
        {
            try
            {
                var stock = _service.Remove(
                    request.ProductId,
                    request.SchoolId,
                    request.Quantity);

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
        [Authorize(Policy = "CanManageStock")]
        public IActionResult Transfer([FromBody] StockTransferRequest request)
        {
            try
            {
                _service.Transfer(
                    request.ProductId,
                    request.OriginSchoolId,
                    request.DestinationSchoolId,
                    request.Quantity);

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
