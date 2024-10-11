using OrderBoard.Contracts.Categories;
using OrderBoard.Domain.Entities;

namespace OrderBoard.AppServices.Categories.Repositories
{
    /// <summary>
    /// Репозиторий для работы с категориями
    /// </summary>
    public interface ICategoryRepository
    {
        /// <summary>
        /// Добавить категорию в БД.
        /// </summary>
        /// <param name="model">Доменная сущность (доменная модель) категории.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Идентификатор добавленной категории.</returns>
        Task<Guid> AddAsync(Category model, CancellationToken cancellationToken);
        /// <summary>
        /// Получить модель категории.
        /// </summary>
        /// <param name="id">Идентификатор категории.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Модель категории.</returns>
        Task<CategoryInfoModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
