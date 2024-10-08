using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OrderBoard.AppServices.User.Services;
using OrderBoard.AppServices.Users.Repository;
using OrderBoard.Contracts.Enums;
using OrderBoard.Contracts.UserDto;
using OrderBoard.Domain.Entities;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OrderBoard.AppServices.Users.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper,
            IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        { 
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;

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
        public Task<UserDataModel> GetByLoginOrEmailAndPasswordAsync(string Login, string email, string Password, CancellationToken cancellationToken)
        {
            return _userRepository.GetByLoginOrEmailAndPasswordAsync(Login, email, Password, cancellationToken);
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

        public async Task<string> LoginAsync(UserAuthDto model, CancellationToken cancellationToken)
        {
            var UserAuthModel = await _userRepository
                .GetByLoginOrEmailAndPasswordAsync(model.Login, model.Email, model.Password, cancellationToken);
            if (UserAuthModel == null)
            {
                throw new Exception("Пользователь не найден");
            }
            if (model.Password != UserAuthModel.Password)
            {
                throw new Exception("Пароль не верен");
            }
            if((model.Login != UserAuthModel.Login) && (model.Email != UserAuthModel.Email))
            {
                throw new Exception("Имя пользователя не сопадает");
            }
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, UserAuthModel.Id.ToString()),
                new Claim(ClaimTypes.Name, UserAuthModel.Name)
            };

            var secretKey = _configuration["Jwt:Key"];
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
                    SecurityAlgorithms.HmacSha256
                )
            );
            var result = new JwtSecurityTokenHandler().WriteToken(token);
            return result.ToString();
        }

        public async Task<UserInfoModel> GetCurrentUserAsync(CancellationToken cancellationToken)
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var claimId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrWhiteSpace(claimId))
            {
                throw new Exception("Непредвиденная ошибка");
            }
            var id = Guid.Parse(claimId);
            var result = await _userRepository.GetByIdAsync(id, cancellationToken);
            if(result == null)
            {
                throw new Exception("Данные о пользователе не найдены");
            }
            return result;
        }
    }
}
