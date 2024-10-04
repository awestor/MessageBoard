using AutoMapper;
using OrderBoard.AppServices.Repository.Repository;
using OrderBoard.AppServices.Repository.Services;
using OrderBoard.Contracts.OrderItem;
using OrderBoard.Domain.Entities;

namespace OrderBoard.AppServices.Users.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IMapper _mapper;

        public OrderItemService(IOrderItemRepository orderItemRepository, IMapper mapper)
        {
            _orderItemRepository = orderItemRepository;
            _mapper = mapper;
        }

        public Task<Guid> CreateAsync(OrderItemCreateModel model, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<OrderItemCreateModel, OrderItem>(model);

            return _orderItemRepository.AddAsync(entity, cancellationToken);
        }

        public Task<OrderItemInfoModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _orderItemRepository.GetByIdAsync(id, cancellationToken);
        }
    }
}
