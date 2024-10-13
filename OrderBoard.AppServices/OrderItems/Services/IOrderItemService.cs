using Microsoft.AspNetCore.Http;
using OrderBoard.Contracts.Items;
using OrderBoard.Contracts.Items.Requests;
using OrderBoard.Contracts.OrderItem;
using OrderBoard.Contracts.OrderItem.Requests;

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
        Task<Guid?> CreateAsync(OrderItemCreateModel model, CancellationToken cancellationToken);
        /// <summary>
        /// Получение по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Модель заказа</returns>
        Task<OrderItemInfoModel> GetByIdAsync(Guid? id, CancellationToken cancellationToken);
        /// <summary>
        /// Вернуть все товары по идентификатору заказа
        /// </summary>
        /// <param name="id">Идентификатор заказа</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Список информационных моделей товаров</returns>
        Task<List<OrderItemInfoModel>> GetAllByOrderIdAsync(Guid? id, CancellationToken cancellationToken);
        /// <summary>
        /// Вернуть все товары по идентификатору заказа
        /// </summary>
        /// <param name="id">Идентификатор заказа</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Список моделей товаров</returns>
        Task<List<OrderItemDataModel>> GetAllByOrderIdInDataModelAsync(Guid? id, CancellationToken cancellationToken);
        /// <summary>
        /// Удаление товара
        /// </summary>
        /// <param name="id">Идентификатор товара</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns></returns>
        Task DeleteByIdAsync(Guid? id, CancellationToken cancellationToken);
        /// <summary>
        /// Временное искусственное каскадное удаление
        /// </summary>
        /// <param name="OrderItemTempModel">Модель товара</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns></returns>
        Task DeleteForOrderDeleteAsync(OrderItemDataModel OrderItemTempModel, CancellationToken cancellationToken);
        Task SetCountAsync(List<OrderItemDataModel> orderItemList, CancellationToken cancellationToken);
        Task UpdateAsync(OrderItemUpdateModel model, CancellationToken cancellationToken);



        /// <summary>
        /// Получить все товары с пагинацией и ограничениями.
        /// </summary>
        /// <param name="model">Входящие ограничения</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Список товаров</returns>
        Task<List<OrderItemInfoModel>> GetItemWithPaginationAsync(SearchOrderItemFromOrderRequest request, CancellationToken cancellationToken);
    }
}
