using OrderBoard.Contracts.Orders;
using OrderBoard.Domain.Entities;


namespace OrderBoard.AppServices.Orders.Repository
{
    public interface IOrderRepository
    {
        Task<Guid> AddAsync(Order model, CancellationToken cancellationToken);
        Task<OrderInfoModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<OrderDataModel> GetForUpdateAsync(Guid id, CancellationToken cancellationToken);
        Task<Guid> UpdateAsync(Order model, CancellationToken cancellationToken);
        Task DeleteByIdAsync(Order model, CancellationToken cancellationToken);
    }
}
