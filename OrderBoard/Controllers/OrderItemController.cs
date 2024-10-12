using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderBoard.AppServices.Other.Exceptions;
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

        public OrderItemController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
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
            var result = await _orderItemService.CreateAsync(model, cancellationToken);
            return StatusCode((int)HttpStatusCode.Created, result);
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
            if (result == null)
            {
                throw new EntitiesNotFoundException("Заказ не найден.");
            }
            return Ok(result);
        }
        /// <summary>
        /// Удаление поля заказа
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns> id подтверждённого заказа</returns>
        [HttpGet("Delete orderItem by orderItemId")]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            await _orderItemService.DeleteByIdAsync(id, cancellationToken);
            return StatusCode((int)HttpStatusCode.OK);
        }
    }
}
