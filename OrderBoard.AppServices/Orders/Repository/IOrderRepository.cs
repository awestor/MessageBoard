using OrderBoard.AppServices.Other.Specifications;
using OrderBoard.Contracts.OrderItem;
using OrderBoard.Contracts.Orders;
using OrderBoard.Domain.Entities;


namespace OrderBoard.AppServices.Orders.Repository
{
    public interface IOrderRepository
    {
        /// <summary>
        /// Добавление заказа
        /// </summary>
        /// <param name="model">Модель заказа</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Идентификатор заказа</returns>
        Task<Guid?> AddAsync(Order model, CancellationToken cancellationToken);
        /// <summary>
        /// Получение по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор заказа</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Модель заказа</returns>
        Task<OrderInfoModel> GetByIdAsync(Guid? id, CancellationToken cancellationToken);
        /// <summary>
        /// Вернуть для обновления
        /// </summary>
        /// <param name="id">Идентификатор заказа</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Модель заказа</returns>
        Task<OrderDataModel> GetForUpdateAsync(Guid? id, CancellationToken cancellationToken);
        /// <summary>
        /// Обновление заказа
        /// </summary>
        /// <param name="model">Модель заказа</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Идентификатор заказа</returns>
        Task<Guid?> UpdateAsync(Order model, CancellationToken cancellationToken);
        /// <summary>
        /// Удаление заказа
        /// </summary>
        /// <param name="model">Модель заказа</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns></returns>
        Task DeleteByModelAsync(Order model, CancellationToken cancellationToken);
        /// <summary>
        /// Вернуть заказ по идентификатору пользователя
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Модель заказа</returns>
        Task<OrderDataModel> GetByUserIdAsync(Guid? id, CancellationToken cancellationToken);
        Task<List<OrderInfoModel>> GetBySpecificationWithPaginationAsync(ISpecification<Order> specification,
            int take, int? skip, CancellationToken cancellationToken);
    }
}
