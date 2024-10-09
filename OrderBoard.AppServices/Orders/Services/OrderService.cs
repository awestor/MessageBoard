using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using OrderBoard.AppServices.Items.Repositories;
using OrderBoard.AppServices.Orders.Repository;
using OrderBoard.AppServices.Users.Repository;
using OrderBoard.Contracts.Items;
using OrderBoard.Contracts.OrderItem;
using OrderBoard.Contracts.Orders;
using OrderBoard.Domain.Entities;
using System.Security.Claims;

namespace OrderBoard.AppServices.Orders.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderService(IOrderRepository orderRepository, IMapper mapper,
            IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        /// <summary>
        /// Добавление нового заказа
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<Guid> CreateAsync(OrderCreateModel model, CancellationToken cancellationToken)
        {
            var OrderModel = _orderRepository.GetByUserIdAsync(model.UserId, cancellationToken);

            if (OrderModel != null)
            {
                throw new Exception("У данного пользователя уже существует наоплаченный заказ. Его Id:" + OrderModel.Id);
            }
            var entity = _mapper.Map<OrderCreateModel, Order>(model);

            return _orderRepository.AddAsync(entity, cancellationToken);
        }
        public async Task<Guid> CreateByAuthAsync(CancellationToken cancellationToken)
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var claimId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrWhiteSpace(claimId))
            {
                throw new Exception("Непредвиденная ошибка");
            }
            var id = Guid.Parse(claimId);
            var OrderModel = await _orderRepository.GetByUserIdAsync(id, cancellationToken);

            if (OrderModel != null)
            {
                throw new Exception("У данного пользователя уже существует наоплаченный заказ. Его Id:" + OrderModel.Id);
            }
            var model = new OrderCreateModel();
            model.UserId = id;
            var entity = _mapper.Map<OrderCreateModel, Order>(model);
            return await _orderRepository.AddAsync(entity, cancellationToken);
        }
        /// <summary>
        /// Получение заказа по id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<OrderInfoModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _orderRepository.GetByIdAsync(id, cancellationToken);
        }
        /// <summary>
        /// Получение модели заказа для обновления
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<OrderDataModel> GetForUpdateAsync(Guid id, CancellationToken cancellationToken)
        {
            return _orderRepository.GetForUpdateAsync(id, cancellationToken);
        }
        /// <summary>
        /// Обновление полей заказа
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<Guid> UpdateAsync(OrderDataModel model, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<OrderDataModel, Order>(model);
            return _orderRepository.UpdateAsync(entity, cancellationToken);
        }
        public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var model = await _orderRepository.GetForUpdateAsync(id, cancellationToken);
            var entity = _mapper.Map<OrderDataModel, Order>(model);
            _orderRepository.DeleteByIdAsync(entity, cancellationToken);
            return;
        }
        /// <summary>
        /// Подтверждение статуса заказа
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Guid> ConfrimOrderById(Guid id, CancellationToken cancellationToken)
        {
            var model = await _orderRepository.GetForUpdateAsync(id, cancellationToken);
            var entity = _mapper.Map<OrderDataModel, Order>(model);
            entity.OrderStatus = Contracts.Enums.OrderStatus.Ordered;
            var result = await _orderRepository.UpdateAsync(entity, cancellationToken);
            return result;
        }
    }
}
