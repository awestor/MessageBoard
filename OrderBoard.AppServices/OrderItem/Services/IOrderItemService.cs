using OrderBoard.Contracts.OrderItem;

namespace OrderBoard.AppServices.Repository.Services
{
    public interface IOrderItemService
    {
        /// <summary>
        /// Создание поля заказа
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Guid> CreateAsync(OrderItemCreateModel model, CancellationToken cancellationToken);
        Task<OrderItemInfoModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<OrderItemInfoModel>> GetAllByOrderIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
