using OrderBoard.Contracts.Items;
using OrderBoard.Contracts.OrderItem;
using OrderBoard.Contracts.OrderItem.Requests;
using OrderBoard.Contracts.Orders;
using OrderBoard.Contracts.Orders.Requests;

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
        Task<Guid?> CreateAsync(OrderCreateModel model, CancellationToken cancellationToken);
        /// <summary>
        /// Создание по авторизации
        /// </summary>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Идентификатор заказа</returns>
        Task<Guid?> CreateByAuthAsync(CancellationToken cancellationToken);
        /// <summary>
        /// Получение по id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<OrderInfoModel> GetByIdAsync(Guid? id, CancellationToken cancellationToken);
        /// <summary>
        /// Обновление записи заказа
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Guid?> UpdateAsync(OrderDataModel model, CancellationToken cancellationToken);
        /// <summary>
        /// Удаление заказа по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор товара</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns></returns>
        Task DeleteByIdAsync(Guid? id, CancellationToken cancellationToken);
        /// <summary>
        /// Удаление заказа по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор товара</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns></returns>
        Task DeleteChildByIdAsync(Guid? id, CancellationToken cancellationToken);
        /// <summary>
        /// Подтверждение заказа
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Guid?> ConfrimOrderById(Guid? id, CancellationToken cancellationToken);
        Task<Guid?> GetOrderIdByUserIdAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить все товары с пагинацией и ограничениями.
        /// </summary>
        /// <param name="model">Входящие ограничения</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Список товаров</returns>
        Task<List<OrderInfoModel>> GetOrderWithPaginationAsync(SearchOrderRequest request, CancellationToken cancellationToken);
        /// <summary>
        /// Получить все товары с пагинацией и ограничениями.
        /// </summary>
        /// <param name="model">Входящие ограничения</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Список товаров</returns>
        Task<List<OrderInfoModel>> GetOrderWithPaginationAuthAsync(SearchOrderAuthRequest request, CancellationToken cancellationToken);

        Task<Guid?> CreateForOrderItemAsync(Guid? userId, CancellationToken cancellationToken);
        Task<OrderInfoModel> SetTotalInfoAsync(OrderDataModel? model, Guid? id, CancellationToken cancellationToken);
    }
}
