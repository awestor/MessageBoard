using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderBoard.AppServices.Items.Services;
using OrderBoard.AppServices.Orders.Services;
using OrderBoard.AppServices.Repository.Services;
using OrderBoard.Contracts.OrderItem;
using System.Net;

namespace OrderBoard.Api.Controllers
{    /// <summary>
     /// Контроллер по работе с полями заказа.
     /// </summary>
     /// <param name="categoryService">Сервис по работе с полями заказа.</param>
    [Route("api/[controller]")]
    [Authorize]
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
        /// <summary>
        /// Дабавления нового поля в заказе
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns> id подтверждённого заказа</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderItemCreateModel model, CancellationToken cancellationToken)
        {
            var ItemTempModel = await _itemService.GetForUpdateAsync(model.ItemId, cancellationToken);
            if (ItemTempModel.Count >= model.Count)
            {
                var OrderTempModel = await _orderService.GetForUpdateAsync(model.OrderId, cancellationToken);

                OrderTempModel.TotalCount += model.Count;
                OrderTempModel.TotalPrice += model.Count * ItemTempModel.Price;
                OrderTempModel.OrderStatus = Contracts.Enums.OrderStatus.Draft;
                await _orderService.UpdateAsync(OrderTempModel, cancellationToken);
                ItemTempModel.Count -= model.Count;
                await _itemService.UpdateAsync(ItemTempModel, cancellationToken);

                var result = await _orderItemService.CreateAsync(model, cancellationToken);
                return StatusCode((int)HttpStatusCode.Created, result);
            }
            else
            return StatusCode((int)HttpStatusCode.BadRequest);
        }
        /// <summary>
        /// Получение OrderItem по его id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns> id подтверждённого заказа</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(OrderItemInfoModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _orderItemService.GetByIdAsync(id, cancellationToken);
            return Ok(result);
        }
        /// <summary>
        /// Получение всех OrderItem из заказа
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns> id подтверждённого заказа</returns>
        [HttpGet("Get all orderItem's by orderId")]
        [ProducesResponseType(typeof(OrderItemInfoModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllByOrderIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await _orderItemService.GetAllByOrderIdAsync(id, cancellationToken);
            return Ok(result);
        }
        /// <summary>
        /// Удаление поля заказа
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns> id подтверждённого заказа</returns>
        [HttpGet("Delete orderItem's by orderItemId")]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var OrderItemTempModel = await _orderItemService.GetByIdAsync(id, cancellationToken);
            if(OrderItemTempModel != null) {
                var ItemTempModel = await _itemService.GetForUpdateAsync(OrderItemTempModel.ItemId, cancellationToken);
                var OrderTempModel = await _orderService.GetForUpdateAsync(OrderItemTempModel.OrderId, cancellationToken);

                OrderTempModel.TotalCount -= OrderItemTempModel.Count;
                OrderTempModel.TotalPrice -= OrderItemTempModel.Count * ItemTempModel.Price;
                await _orderService.UpdateAsync(OrderTempModel, cancellationToken);
                ItemTempModel.Count += OrderItemTempModel.Count;
                await _itemService.UpdateAsync(ItemTempModel, cancellationToken);
                await _orderItemService.DeleteByIdAsync(id, cancellationToken);
                return StatusCode((int)HttpStatusCode.OK);
            }
            return StatusCode((int)HttpStatusCode.BadRequest);
        }
    }
}
