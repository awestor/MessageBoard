using OrderBoard.Contracts.Items;
using OrderBoard.Contracts.UserDto;

namespace OrderBoard.AppServices.User.Services
{
    public interface IUserService
    {
        Task<Guid> CreateAsync(UserCreateModel model, CancellationToken cancellationToken);
        Task<UserInfoModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
