using OrderBoard.Contracts.Enums;
using OrderBoard.Contracts.Items;
using OrderBoard.Contracts.UserDto;

namespace OrderBoard.AppServices.User.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Создать пользователя
        /// </summary>
        /// <param name="model">Доменная сущность пользователя</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Идентификатор добавленной пользователя</returns>
        Task<Guid> CreateAsync(UserCreateModel model, CancellationToken cancellationToken);
        /// <summary>
        /// Получить модель пользователя.
        /// </summary>
        /// <param name="id">Идентификатор категории.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Модель пользователя.</returns>
        Task<UserInfoModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        /// <summary>
        /// Обновление пользователя
        /// </summary>
        /// <param name="model">Модель пользователя</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Идентификатор пользователя</returns>
        Task<Guid> UpdateAsync(UserDataModel model, CancellationToken cancellationToken);
        /// <summary>
        /// Удаление товара по идентификатору
        /// </summary>
        /// <param name="model">Модель пользователя</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
        /// <summary>
        /// Вернуть для обновления
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Модель пользователя</returns>
        Task<UserDataModel> GetForUpdateAsync(Guid id, CancellationToken cancellationToken);
        /// <summary>
        /// Установаить роль
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <param name="role">Роль</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Идентификатор пользователя</returns>
        Task<Guid> SetRoleAsync(Guid id, UserRole role, CancellationToken cancellationToken);
        /// <summary>
        /// Получить токен для авторизации
        /// </summary>
        /// <param name="model">Модель пользователя</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Токен</returns>
        Task<String> LoginAsync(UserAuthDto model, CancellationToken cancellationToken);
        /// <summary>
        /// Аутентификация пользователя
        /// </summary>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Информационная модель пользователя</returns>
        Task<UserInfoModel> GetCurrentUserAsync(CancellationToken cancellationToken);
        /// <summary>
        /// Получить модель пользователя по паролю + логину или почте
        /// </summary>
        /// <param name="Login">Логин пользователя</param>
        /// <param name="Email">Почта пользователя</param>
        /// <param name="Password">Пароль пользователя</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Модель пользователя</returns>
        Task<UserDataModel> GetByLoginOrEmailAndPasswordAsync(string Login, string Email, string Password, CancellationToken cancellationToken);
    }
}
