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
        Task<UserDataModel> GetForUpdateAsync(Guid id, CancellationToken cancellationToken);
        Task<Guid> SetRoleAsync(Guid id, UserRole role, CancellationToken cancellationToken);

    }
}
