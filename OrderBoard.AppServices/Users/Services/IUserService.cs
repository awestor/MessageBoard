using OrderBoard.Contracts.Enums;
using OrderBoard.Contracts.Items;
using OrderBoard.Contracts.Orders;
using OrderBoard.Contracts.UserDto;
using OrderBoard.Contracts.UserDto.Requests;

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
        Task<Guid?> CreateAsync(UserCreateModel model, CancellationToken cancellationToken);
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
        Task<Guid?> UpdateAsync(UserUpdateInputModel model, CancellationToken cancellationToken);
        /// <summary>
        /// Установаить роль
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <param name="role">Роль</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Идентификатор пользователя</returns>
        Task<Guid?> SetRoleAsync(Guid? id, string setRole, CancellationToken cancellationToken);
        /// <summary>
        /// Получить токен для авторизации
        /// </summary>
        /// <param name="model">Модель пользователя</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Токен</returns>
        Task<String> LoginAsync(UserLoginAuthRequest? loginModel, UserEmailAuthRequest? emailModel, CancellationToken cancellationToken);
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
        Task<UserDataModel> GetUserByLoginAndPasswordAsync(UserLoginAuthRequest model, CancellationToken cancellationToken);
        Task<UserDataModel> GetUserByEmailAndPasswordAsync(UserEmailAuthRequest model, CancellationToken cancellationToken);

        Task DeleteByIdAsync(Guid? id, CancellationToken cancellationToken);
        Task DeleteAuthAsync(CancellationToken cancellationToken);
    }
}
