using OrderBoard.AppServices.Other.Specifications;
using OrderBoard.Contracts.OrderItem;
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
        Task<Guid?> AddAsync(OrderItem model, CancellationToken cancellationToken);
        Task<List<OrderItemInfoModel>> GetBySpecificationWithPaginationAsync(ISpecification<OrderItem> specification,
           int take, int? skip, CancellationToken cancellationToken);
        Task<OrderItemDataModel?> GetDataBySpecificationAsync(ISpecification<OrderItem> specification,
            CancellationToken cancellationToken);

        /// <summary>
        /// Вернуть все товары по идентификатору заказа
        /// </summary>
        /// <param name="id">Идентификатор заказа</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Список информационных моделей товаров</returns>
        Task<List<OrderItemInfoModel>> GetAllInfoByOrderIdAsync(Guid? id, CancellationToken cancellationToken);
        /// <summary>
        /// Вернуть все товары по идентификатору заказа
        /// </summary>
        /// <param name="id">Идентификатор заказа</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Список моделей товаров</returns>
        Task<List<OrderItemDataModel>> GetAllDataByOrderIdAsync(Guid? id, CancellationToken cancellationToken);

        /// <summary>
        /// Получение по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор товара</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Модель товара</returns>
        Task<OrderItemInfoModel> GetInfoByIdAsync(Guid? id, CancellationToken cancellationToken);
        /// <summary>
        /// Вернуть для обнавления
        /// </summary>
        /// <param name="id">Идентификатор заказа</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Модель товара</returns>
        Task<OrderItemDataModel> GetDataByIdAsync(Guid? id, CancellationToken cancellationToken);

        /// <summary>
        /// Обновление товара
        /// </summary>
        /// <param name="model">Модель товара</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Идентификатор товара</returns>
        Task<Guid?> UpdateAsync(OrderItem model, CancellationToken cancellationToken);
        /// <summary>
        /// Удаление товара
        /// </summary>
        /// <param name="model">Модель товара</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns></returns>
        Task DeleteAsync(OrderItem model, CancellationToken cancellationToken);
        Task<List<OrderItemDataModel>> GetAllDataByItemIdAsync(Guid? id, CancellationToken cancellationToken);
    }
}
