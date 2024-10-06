using OrderBoard.Contracts.Orders;

namespace OrderBoard.AppServices.Orders.Services
{
    public interface IOrderService
    {
        Task<Guid> CreateAsync(OrderCreateModel model, CancellationToken cancellationToken);
        Task<OrderInfoModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<OrderDataModel> GetForUpdateAsync(Guid id, CancellationToken cancellationToken);
    }
}
