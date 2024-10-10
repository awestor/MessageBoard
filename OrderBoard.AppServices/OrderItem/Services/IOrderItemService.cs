using Microsoft.AspNetCore.Http;
using OrderBoard.Contracts.Items;
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
        Task<List<OrderItemDataModel>> GetAllByOrderIdInDataModelAsync(Guid id, CancellationToken cancellationToken);
        Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
        Task DeleteForOrderDeleteAsync(OrderItemDataModel OrderItemTempModel, CancellationToken cancellationToken);
        Task SetCountAsync(ItemDataModel ItemTempModel, decimal count, bool check, CancellationToken cancellationToken);
        Task<ItemDataModel> GetItemClassAsync(Guid ItemId, CancellationToken cancellationToken);
    }
}
