using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderBoard.AppServices.Orders.Services;
using OrderBoard.Contracts.Orders;
using System.Net;

namespace OrderBoard.Api.Controllers
{
    /// <summary>
    /// Контроллер по работе с заказами.
    /// </summary>
    /// <param name="categoryService">Сервис по работе с заказами.</param>
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        /// <summary>
        /// Создание нового заказа
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>id созданного заказа</returns>
        [HttpPost("Create New Order")]
        public async Task<IActionResult> Post([FromBody] OrderCreateModel model, CancellationToken cancellationToken)
        {
            var result = await _orderService.CreateAsync(model, cancellationToken);
            return StatusCode((int)HttpStatusCode.Created, result);
        }
        /// <summary>
        /// Получение заказа по id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(OrderInfoModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _orderService.GetByIdAsync(id, cancellationToken);
            return Ok(result);
        }
        /// <summary>
        /// Подтверждение заказа
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns> id подтверждённого заказа</returns>
        [HttpGet("Confrim Order")]
        [ProducesResponseType(typeof(OrderInfoModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> ConfrimOrderById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _orderService.ConfrimOrderById(id, cancellationToken);
            return Ok(result);
        }

    }
}
