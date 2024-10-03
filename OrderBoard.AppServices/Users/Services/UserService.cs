using AutoMapper;
using OrderBoard.AppServices.User.Services;
using OrderBoard.AppServices.Users.Repository;
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
    }
}
