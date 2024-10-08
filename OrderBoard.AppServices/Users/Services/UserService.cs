using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OrderBoard.AppServices.Orders.Repository;
using OrderBoard.AppServices.User.Services;
using OrderBoard.AppServices.Users.Repository;
using OrderBoard.Contracts.Enums;
using OrderBoard.Contracts.Orders;
using OrderBoard.Contracts.UserDto;
using OrderBoard.Domain.Entities;

namespace OrderBoard.AppServices.Users.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public Task<Guid> CreateAsync(UserCreateModel model, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<UserCreateModel, EntUser>(model);

            return _userRepository.AddAsync(entity, cancellationToken);
        }

        public Task<UserInfoModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _userRepository.GetByIdAsync(id, cancellationToken);
        }
        public Task<UserDataModel> GetForUpdateAsync(Guid id, CancellationToken cancellationToken)
        {
            return _userRepository.GetForUpdateAsync(id, cancellationToken);
        }
        public Task<Guid> UpdateAsync(UserDataModel model, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<UserDataModel, EntUser>(model);
            return _userRepository.UpdateAsync(entity, cancellationToken);
        }
        public async Task<Guid> SetRoleAsync(Guid id, UserRole role, CancellationToken cancellationToken)
        {
            var model = await GetForUpdateAsync(id, cancellationToken);
            model.Role = role;
            var entity = await UpdateAsync(model, cancellationToken);

            return entity;
        }
    }
}
