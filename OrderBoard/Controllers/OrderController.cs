using Microsoft.AspNetCore.Mvc;
using OrderBoard.AppServices.Orders.Services;
using OrderBoard.Contracts.Orders;
using System.Net;

namespace OrderBoard.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderCreateModel model, CancellationToken cancellationToken)
        {
            var result = await _orderService.CreateAsync(model, cancellationToken);
            return StatusCode((int)HttpStatusCode.Created, result);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(OrderInfoModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _orderService.GetByIdAsync(id, cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(OrderInfoModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetForUpdateAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await _orderService.GetForUpdateAsync(id, cancellationToken);
            return Ok(result);
        }
    }
}
