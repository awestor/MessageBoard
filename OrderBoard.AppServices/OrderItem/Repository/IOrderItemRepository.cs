﻿using OrderBoard.Contracts.OrderItem;
using OrderBoard.Contracts.Orders;
using OrderBoard.Domain.Entities;


namespace OrderBoard.AppServices.Repository.Repository
{
    public interface IOrderItemRepository
    {
        Task<Guid> AddAsync(OrderItem model, CancellationToken cancellationToken);
        Task<OrderItemInfoModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<OrderItemInfoModel>> GetAllByOrderIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<OrderItemDataModel>> GetAllByOrderIdInDataModelAsync(Guid id, CancellationToken cancellationToken);
        Task<OrderItemDataModel> GetForAddAsync(Guid itemId, Guid orderId, CancellationToken cancellationToken);
        Task<OrderItemDataModel> GetForUpdateAsync(Guid id, CancellationToken cancellationToken);
        Task<Guid> UpdateAsync(OrderItem model, CancellationToken cancellationToken);
        Task DeleteByModelAsync(OrderItem model, CancellationToken cancellationToken);
    }
}
