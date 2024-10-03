using OrderBoard.Contracts.UserDto;
using OrderBoard.Domain.Entities;


namespace OrderBoard.AppServices.Users.Repository
{
    public interface IUserRepository
    {
        Task<Guid> AddAsync(EntUser model, CancellationToken cancellationToken);
        Task<UserInfoModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
