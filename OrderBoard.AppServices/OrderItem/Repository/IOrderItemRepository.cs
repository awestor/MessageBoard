using OrderBoard.Contracts.OrderItem;
using OrderBoard.Domain.Entities;


namespace OrderBoard.AppServices.Repository.Repository
{
    public interface IOrderItemRepository
    {
        Task<Guid> AddAsync(OrderItem model, CancellationToken cancellationToken);
        Task<OrderItemInfoModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<OrderItemInfoModel>> GetAllByOrderIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
