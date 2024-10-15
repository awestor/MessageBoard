using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using OrderBoard.AppServices.Items.Repositories;
using OrderBoard.AppServices.Other.Exceptions;
using OrderBoard.AppServices.Other.Generators;
using OrderBoard.AppServices.Other.Hasher;
using OrderBoard.AppServices.Other.Services;
using OrderBoard.AppServices.Other.Validators.Users;
using OrderBoard.AppServices.User.Services;
using OrderBoard.AppServices.Users.Repository;
using OrderBoard.AppServices.Users.SpecificationContext.Builders;
using OrderBoard.Contracts.Enums;
using OrderBoard.Contracts.Items;
using OrderBoard.Contracts.UserDto;
using OrderBoard.Contracts.UserDto.Requests;
using OrderBoard.Domain.Entities;

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
        private readonly IItemRepository _itemRepository;
        private readonly IUserSpecificationBuilder _userSpecificationBuilder;
        private readonly IStructuralLoggingService _structuralLoggingService;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, IMapper mapper,
            IConfiguration configuration, IHttpContextAccessor httpContextAccessor,
            IItemRepository itemRepository, IUserSpecificationBuilder userSpecificationBuilder,
            IStructuralLoggingService structuralLoggingService,
            ILogger<UserService> logger)
        { 
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _itemRepository = itemRepository;
            _userSpecificationBuilder = userSpecificationBuilder;
            _structuralLoggingService = structuralLoggingService;
            _logger = logger;

        }

        public async Task<Guid?> CreateAsync(UserCreateModel model, CancellationToken cancellationToken)
        {
            model.Password = CryptoHasher.GetBase64Hash(model.Password);
            var specification = _userSpecificationBuilder.BuildCheckLogin(model.Login);
            var result = await _userRepository.GetDataBySpecificationAsync(specification, cancellationToken);
            if (result != null) { throw new EntititysNotVaildException("Данный e-mail уже занят"); }

            specification = _userSpecificationBuilder.BuildCheckEmail(model.Login);
            result = await _userRepository.GetDataBySpecificationAsync(specification, cancellationToken);
            if (result != null) { throw new EntititysNotVaildException("Данный login уже занят"); }

            var entity = _mapper.Map<UserCreateModel, EntUser>(model);

            _structuralLoggingService.PushProperty("CreateRequest", model);
            _logger.LogInformation("Пользователь был зарегестрирован.");
            return await _userRepository.AddAsync(entity, cancellationToken);
        }

        public async Task<UserInfoModel> GetByIdAsync(Guid? id, CancellationToken cancellationToken)
        {
            var result = await _userRepository.GetByIdAsync(id, cancellationToken);
            _structuralLoggingService.PushProperty("GetRequest", id);
            _logger.LogInformation("Пользователь был получен.");
            return result;
        }
        public async Task<Guid?> UpdateAsync(UserUpdateInputModel model, CancellationToken cancellationToken)
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var claimId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var UserModel = await _userRepository.GetDataByIdAsync(new Guid(claimId), cancellationToken);
            UserModel = UpdateUserValidator.UpdateValidator(UserModel, model);
            if (UserModel == null || UserModel.Role==UserRole.Deleted) 
            {
                throw new EntitiesNotFoundException("Вашь профиль не найден или был удалён.");
            }

            var specification = _userSpecificationBuilder.BuildCheckLogin(model.Login);
            var check = await _userRepository.GetDataBySpecificationAsync(specification, cancellationToken);
            if (check != null) { throw new EntititysNotVaildException("Данный e-mail уже занят"); }

            specification = _userSpecificationBuilder.BuildCheckEmail(model.Login);
            check = await _userRepository.GetDataBySpecificationAsync(specification, cancellationToken);
            if (check != null) { throw new EntititysNotVaildException("Данный login уже занят"); }

            var entity = _mapper.Map<UserDataModel, EntUser>(UserModel);

            _structuralLoggingService.PushProperty("UpdateRequest", model);
            _logger.LogInformation("Информация о пользователе была изменена.");
            return await _userRepository.UpdateAsync(entity, cancellationToken);
        }
        public async Task<Guid?> SetRoleAsync(Guid? id, string setRole, CancellationToken cancellationToken)
        {
            UserRole role;
            if (setRole == "Admin" || setRole == "2")
            {
                role = UserRole.Admin;
            }
            else if (setRole == "Authorized" || setRole == "1")
            {
                role = UserRole.Authorized;
            }
            else throw new EntititysNotVaildException(nameof(setRole) + "Не подходящее значение.");
            var model = await _userRepository.GetDataByIdAsync(id, cancellationToken)
                ?? throw new EntitiesNotFoundException("Пользователь, которому необходимо изменить роль не найден.");
            if(model.Role == role)
            {
                throw new EntititysNotVaildException("У данного пользователя уже есть данный статус");
            }
            model.Role = role;
            var entity = _mapper.Map<UserDataModel, EntUser>(model);
            var result = await _userRepository.UpdateAsync(entity, cancellationToken);

            _structuralLoggingService.PushProperty("SetRequest", id);
            _logger.LogInformation("Роль пользователя была изменена.");
            return result;
        }

        public async Task<UserInfoModel> GetCurrentUserAsync(CancellationToken cancellationToken)
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var claimId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value
                ?? throw new Exception("Пожалуйста авторизуйтесь заново.");
            if (string.IsNullOrWhiteSpace(claimId))
            {
                throw new Exception("Непредвиденная ошибка");
            }
            var id = Guid.Parse(claimId);
            var result = await _userRepository.GetByIdAsync(id, cancellationToken);
            if(result == null)
            {
                throw new EntitiesNotFoundException("Данные о пользователе не найдены");
            }
            return result;
        }

        public async Task DeleteByIdAsync(Guid? id, CancellationToken cancellationToken)
        {
            var modelList = await _itemRepository.GetAllItemAsync(id, cancellationToken);
            await SetNullChildItemAsync(modelList, cancellationToken);
            var modelToDelete = await _userRepository.GetDataByIdAsync(id, cancellationToken);
            modelToDelete.PhoneNumber = null;
            modelToDelete.Email = EmailGenerator.generateEmail(40);
            modelToDelete.Login = loginGenerator.generateLogin(40);
            modelToDelete.Role = UserRole.Deleted;
            modelToDelete.Password = CryptoHasher.GetBase64Hash(PasswordGenerator.generatePassword(80));
            var entity = _mapper.Map<UserDataModel, EntUser>(modelToDelete);
            await _userRepository.UpdateAsync(entity, cancellationToken);

            _structuralLoggingService.PushProperty("DeleteRequest", id);
            _logger.LogInformation("Пользователь был отмечен как удалённый.");
            return;
        }

        public async Task DeleteAuthAsync(CancellationToken cancellationToken)
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var claimId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value
                ?? throw new Exception("Пожалуйста авторизуйтесь заново.");
            await DeleteByIdAsync(Guid.Parse(claimId), cancellationToken);
            return;
        }

        public async Task SetNullChildItemAsync(List<ItemDataModel> modelList, CancellationToken cancellationToken)
        {
            foreach (var item in modelList)
            {
                item.Count = 0;
                var entity = _mapper.Map<ItemDataModel, Item>(item);
                await _itemRepository.UpdateAsync(entity, cancellationToken);
            }
            return;
        }

        public async Task<string> LoginAsync(UserLoginAuthRequest? loginModel, UserEmailAuthRequest? emailModel, CancellationToken cancellationToken)
        {
            UserDataModel UserAuthModel;
            if (loginModel != null)
            {
                UserAuthModel = await GetUserByLoginAndPasswordAsync(loginModel, cancellationToken);
            }
            else if (emailModel != null)
            {
                UserAuthModel = await GetUserByEmailAndPasswordAsync(emailModel, cancellationToken);
            }
            else throw new EntitiesNotFoundException("Информация о пользователе не найдена или был отправлен неверный набор данных");
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, UserAuthModel.Id.ToString()),
                new Claim(ClaimTypes.Name, UserAuthModel.Name),
                new Claim(ClaimTypes.Role, UserAuthModel.Role.ToString())
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

            _structuralLoggingService.PushProperty("AuthRequest", result);
            _logger.LogInformation("Пользователь был идетефицирован и аудетефицирован.");
            return result.ToString();
        }

        public async Task<UserDataModel> GetUserByLoginAndPasswordAsync(UserLoginAuthRequest model, CancellationToken cancellationToken)
        {
            var specification = _userSpecificationBuilder.BuildLogin(model.Login, model.Password);
            var result = await _userRepository.GetDataBySpecificationAsync(specification, cancellationToken);
            if(result == null) throw new EntitiesNotFoundException("Пользователь не найден");
            return result;
        }

        public async Task<UserDataModel> GetUserByEmailAndPasswordAsync(UserEmailAuthRequest model, CancellationToken cancellationToken)
        {
            var specification = _userSpecificationBuilder.BuildEmail(model.Email, model.Password);
            var result = await _userRepository.GetDataBySpecificationAsync(specification, cancellationToken);
            if (result == null) throw new EntitiesNotFoundException("Пользователь не найден");
            return result;
        }
    }
}
