using OrderBoard.Contracts.Enums;
using OrderBoard.Contracts.Items;
using OrderBoard.Contracts.UserDto;

namespace OrderBoard.AppServices.User.Services
{
    public interface IUserService
    {
        Task<Guid> CreateAsync(UserCreateModel model, CancellationToken cancellationToken);
        Task<UserInfoModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<Guid> UpdateAsync(UserDataModel model, CancellationToken cancellationToken);
        Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<UserDataModel> GetForUpdateAsync(Guid id, CancellationToken cancellationToken);
        Task<Guid> SetRoleAsync(Guid id, UserRole role, CancellationToken cancellationToken);
        Task<String> LoginAsync(UserAuthDto model, CancellationToken cancellationToken);
        Task<UserInfoModel> GetCurrentUserAsync(CancellationToken cancellationToken);
        Task<UserDataModel> GetByLoginOrEmailAndPasswordAsync(string Login, string Email, string Password, CancellationToken cancellationToken);
    }
}
