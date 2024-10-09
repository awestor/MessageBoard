using OrderBoard.Contracts.Enums;
using OrderBoard.Contracts.UserDto;
using OrderBoard.Domain.Entities;


namespace OrderBoard.AppServices.Users.Repository
{
    public interface IUserRepository
    {
        Task<Guid> AddAsync(EntUser model, CancellationToken cancellationToken);
        Task<UserInfoModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<Guid> UpdateAsync(EntUser model, CancellationToken cancellationToken);
        Task<UserDataModel> GetForUpdateAsync(Guid id, CancellationToken cancellationToken);
        Task DeleteByIdAsync(EntUser model, CancellationToken cancellationToken);
        Task<UserDataModel> GetByLoginOrEmailAndPasswordAsync(string login, string email,  string password, CancellationToken cancellationToken);
    }
}
