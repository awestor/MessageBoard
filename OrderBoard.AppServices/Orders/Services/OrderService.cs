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

        public Task<Guid> CreateAsync(OrderCreateModel model, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<OrderCreateModel, Order>(model);

            return _orderRepository.AddAsync(entity, cancellationToken);
        }

        public Task<OrderInfoModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _orderRepository.GetByIdAsync(id, cancellationToken);
        }

        public Task<OrderDataModel> GetForUpdateAsync(Guid id, CancellationToken cancellationToken)
        {
            return _orderRepository.GetForUpdateAsync(id, cancellationToken);
        }
        public Task<Guid> UpdateAsync(OrderDataModel model, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<OrderDataModel, Order>(model);
            return _orderRepository.UpdateAsync(entity, cancellationToken);
        }
    }
}
