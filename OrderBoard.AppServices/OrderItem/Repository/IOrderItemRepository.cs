using OrderBoard.Contracts.OrderItem;
using OrderBoard.Contracts.Orders;
using OrderBoard.Domain.Entities;


namespace OrderBoard.AppServices.Repository.Repository
{
    public interface IOrderItemRepository
    {
        /// <summary>
        /// Добавление товара
        /// </summary>
        /// <param name="model">Модель товара</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Идентификатор товара</returns>
        Task<Guid> AddAsync(OrderItem model, CancellationToken cancellationToken);
        /// <summary>
        /// Получение по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор товара</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Модель товара</returns>
        Task<OrderItemInfoModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        /// <summary>
        /// Вернуть все товары по идентификатору заказа
        /// </summary>
        /// <param name="id">Идентификатор заказа</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Список информационных моделей товаров</returns>
        Task<List<OrderItemInfoModel>> GetAllByOrderIdAsync(Guid id, CancellationToken cancellationToken);
        /// <summary>
        /// Вернуть все товары по идентификатору заказа
        /// </summary>
        /// <param name="id">Идентификатор заказа</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Список моделей товаров</returns>
        Task<List<OrderItemDataModel>> GetAllByOrderIdInDataModelAsync(Guid id, CancellationToken cancellationToken);
        /// <summary>
        /// Получить для добавления
        /// </summary>
        /// <param name="itemId">Идентификартор товара</param>
        /// <param name="orderId">Идентификартор заказа</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Модель товара</returns>
        Task<OrderItemDataModel> GetForAddAsync(Guid itemId, Guid orderId, CancellationToken cancellationToken);
        /// <summary>
        /// Вернуть для обнавления
        /// </summary>
        /// <param name="id">Идентификатор заказа</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Модель товара</returns>
        Task<OrderItemDataModel> GetForUpdateAsync(Guid id, CancellationToken cancellationToken);
        /// <summary>
        /// Обновление товара
        /// </summary>
        /// <param name="model">Модель товара</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Идентификатор товара</returns>
        Task<Guid> UpdateAsync(OrderItem model, CancellationToken cancellationToken);
        /// <summary>
        /// Удаление товара
        /// </summary>
        /// <param name="model">Модель товара</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns></returns>
        Task DeleteByModelAsync(OrderItem model, CancellationToken cancellationToken);
    }
}
