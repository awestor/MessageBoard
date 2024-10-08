using AutoMapper;
using OrderBoard.AppServices.Orders.Repository;
using OrderBoard.Contracts.OrderItem;
using OrderBoard.Contracts.Orders;
using OrderBoard.Domain.Entities;

namespace OrderBoard.AppServices.Orders.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }
        /// <summary>
        /// Добавление нового заказа
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<Guid> CreateAsync(OrderCreateModel model, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<OrderCreateModel, Order>(model);

            return _orderRepository.AddAsync(entity, cancellationToken);
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
