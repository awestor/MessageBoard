using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderBoard.AppServices.Items.Services;
using OrderBoard.AppServices.Orders.Services;
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
        private readonly IOrderService _orderService;
        private readonly IItemService _itemService;
        public OrderItemController(IOrderItemService orderItemService, IOrderService orderService, IItemService itemService)
        {
            _orderItemService = orderItemService;
            _orderService = orderService;
            _itemService = itemService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderItemCreateModel model, CancellationToken cancellationToken)
        {
            var result = await _orderItemService.CreateAsync(model, cancellationToken);
            var OrderTempModel = await _orderService.GetForUpdateAsync(model.OrderId, cancellationToken);
            var ItemTempModel = await _itemService.GetForUpdateAsync(model.ItemId, cancellationToken);
            OrderTempModel.TotalCount += model.Count;
            OrderTempModel.TotalPrice += model.Count * ItemTempModel.Price;

            return StatusCode((int)HttpStatusCode.Created, OrderTempModel);
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
