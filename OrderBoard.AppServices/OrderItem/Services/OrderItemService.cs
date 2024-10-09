using AutoMapper;
using Microsoft.AspNetCore.Http;
using OrderBoard.AppServices.Items.Services;
using OrderBoard.AppServices.Orders.Repository;
using OrderBoard.AppServices.Orders.Services;
using OrderBoard.AppServices.Repository.Repository;
using OrderBoard.AppServices.Repository.Services;
using OrderBoard.Contracts.OrderItem;
using OrderBoard.Contracts.Orders;
using OrderBoard.Domain.Entities;
using System.Net;

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
            var result =  _orderItemRepository.GetByIdAsync(id, cancellationToken);
            return result;
        }
        public Task<List<OrderItemInfoModel>> GetAllByOrderIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _orderItemRepository.GetAllByOrderIdAsync(id, cancellationToken);
        }
        public Task<OrderItemDataModel> GetForUpdateAsync(Guid id, CancellationToken cancellationToken)
        {
            return _orderItemRepository.GetForUpdateAsync(id, cancellationToken);
        }
        public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var model = await _orderItemRepository.GetForUpdateAsync(id, cancellationToken);
            var entity = _mapper.Map<OrderItemDataModel, OrderItem>(model);
            _orderItemRepository.DeleteByIdAsync(entity, cancellationToken);
            return;
        }

    }
}
