using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderBoard.AppServices.Repository.Services;
using OrderBoard.AppServices.Users.Services;
using OrderBoard.Contracts.OrderItem;
using System.Net;

namespace OrderBoard.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService _orderItemService;
        public OrderItemController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderItemCreateModel model, CancellationToken cancellationToken)
        {
            var result = await _orderItemService.CreateAsync(model, cancellationToken);
            return StatusCode((int)HttpStatusCode.Created, result);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(OrderItemInfoModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _orderItemService.GetByIdAsync(id, cancellationToken);
            return Ok(result);
        }
    }
}
