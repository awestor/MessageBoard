using OrderBoard.Contracts.OrderItem;

namespace OrderBoard.AppServices.Repository.Services
{
    public interface IOrderItemService
    {
        Task<Guid> CreateAsync(OrderItemCreateModel model, CancellationToken cancellationToken);
        Task<OrderItemInfoModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
