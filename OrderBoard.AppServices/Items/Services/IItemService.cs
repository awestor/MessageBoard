using OrderBoard.Contracts.Items;
using OrderBoard.Contracts.Items.Requests;
using OrderBoard.Contracts.Orders;
using OrderBoard.Domain.Entities;

namespace OrderBoard.AppServices.Items.Services
{
    public interface IItemService
    {
        /// <summary>
        /// Создание сущности.
        /// </summary>
        /// <param name="model">Модель.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Идентификатор сохранённой сущности.</returns>
        Task<Guid?> CreateAsync(ItemCreateModel model, CancellationToken cancellationToken);
        /// <summary>
        /// Получить модель товара.
        /// </summary>
        /// <param name="id">Идентификатор товара.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Модель товара.</returns>
        Task<ItemInfoModel> GetByIdAsync(Guid? id, CancellationToken cancellationToken);
        /// <summary>
        /// Получить все товары с пагинацией и ограничениями.
        /// </summary>
        /// <param name="model">Входящие ограничения</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Список товаров</returns>
        Task<List<ItemInfoModel>> GetItemWithPaginationAsync(SearchItemForPaginationRequest request, CancellationToken cancellationToken);
        /// <summary>
        /// Получить все товары по идентефикатору категории.
        /// </summary>
        /// <param name="id">Идентификатор категории.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Модель товара.</returns>
        Task<List<ItemInfoModel>> GetByCategoryIdAsync(Guid? id, CancellationToken cancellationToken);
        /// <summary>
        /// Обновить товар по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор товара</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Модель товара</returns>
        Task<ItemDataModel> GetForUpdateAsync(Guid? id, CancellationToken cancellationToken);
        /// <summary>
        /// Обновить товар по модели
        /// </summary>
        /// <param name="model">Модель товара.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Идентификатор товара</returns>
        Task<Guid?> UpdateAsync(ItemUpdateModel model, CancellationToken cancellationToken);
        Task<Guid?> UpdateAsync(ItemDataModel model, CancellationToken cancellationToken);
        /// <summary>
        /// Удаление по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор товара.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns></returns>
        Task DeleteByIdAsync(Guid? id, CancellationToken cancellationToken);
        Task<Decimal?> GetPriceAsync(Guid? id, CancellationToken cancellationToken);
        Task<List<ItemInfoModel>> GetAllItemAsync(CancellationToken cancellationToken);
    }
}
