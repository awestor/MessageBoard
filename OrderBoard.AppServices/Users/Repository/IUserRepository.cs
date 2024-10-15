using OrderBoard.AppServices.Other.Specifications;
using OrderBoard.Contracts.Enums;
using OrderBoard.Contracts.UserDto;
using OrderBoard.Domain.Entities;


namespace OrderBoard.AppServices.Users.Repository
{
    public interface IUserRepository
    {
        /// <summary>
        /// Добавить пользователя
        /// </summary>
        /// <param name="model">Доменная сущность пользователя</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Идентификатор добавленной пользователя</returns>
        Task<Guid?> AddAsync(EntUser model, CancellationToken cancellationToken);
        /// <summary>
        /// Получить модель пользователя.
        /// </summary>
        /// <param name="id">Идентификатор категории.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Модель пользователя.</returns>
        Task<UserInfoModel> GetByIdAsync(Guid? id, CancellationToken cancellationToken);
        /// <summary>
        /// Обновление пользователя
        /// </summary>
        /// <param name="model">Модель пользователя</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Идентификатор пользователя</returns>
        Task<Guid?> UpdateAsync(EntUser model, CancellationToken cancellationToken);
        /// <summary>
        /// Вернуть для обновления
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Модель пользователя</returns>
        Task<UserDataModel> GetDataByIdAsync(Guid? id, CancellationToken cancellationToken);
        /// <summary>
        /// Удаление товара
        /// </summary>
        /// <param name="model">Модель пользователя</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        Task DeleteAsync(EntUser model, CancellationToken cancellationToken);
        /// <summary>
        /// Удаление товара по идентификатору
        /// </summary>
        /// <param name="model">Модель пользователя</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        Task DeleteByIdAsync(Guid? id, CancellationToken cancellationToken);

        Task<UserDataModel?> GetDataBySpecificationAsync(ISpecification<EntUser> specification,
            CancellationToken cancellationToken);
    }
}
