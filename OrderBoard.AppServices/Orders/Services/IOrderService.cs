using OrderBoard.Contracts.Orders;

namespace OrderBoard.AppServices.Orders.Services
{
    public interface IOrderService
    {
        /// <summary>
        /// Добавление нового заказа
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Guid> CreateAsync(OrderCreateModel model, CancellationToken cancellationToken);
        /// <summary>
        /// Получение по id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<OrderInfoModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        /// <summary>
        /// Получение модели для обновления
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<OrderDataModel> GetForUpdateAsync(Guid id, CancellationToken cancellationToken);
        /// <summary>
        /// Обновление записи заказа
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Guid> UpdateAsync(OrderDataModel model, CancellationToken cancellationToken);
        Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
        /// <summary>
        /// Подтверждение заказа
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Guid> ConfrimOrderById(Guid id, CancellationToken cancellationToken);
    }
}
