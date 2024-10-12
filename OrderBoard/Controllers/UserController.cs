using OrderBoard.AppServices.User.Services;
using Microsoft.AspNetCore.Mvc;
using OrderBoard.Contracts.UserDto;
using System.Net;
using OrderBoard.Contracts.Enums;
using Microsoft.AspNetCore.Authorization;
using OrderBoard.AppServices.Other.Exceptions;
using OrderBoard.Contracts.OrderItem;

namespace OrderBoard.Api.Controllers
{

    [ApiController]
    [Route(template:"[controller]")]
    public class UserController: ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserCreateModel model, CancellationToken cancellationToken)
        {
            var result = await _userService.CreateAsync(model, cancellationToken);
            return StatusCode((int)HttpStatusCode.Created, result);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(UserInfoModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _userService.GetByIdAsync(id, cancellationToken);
            return Ok(result);
        }

        [HttpPost("{id:guid} Change user role in DB")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> SetRole(Guid id, string setRole, CancellationToken cancellationToken)
        { 
            UserRole role;
            if (setRole == "Admin")
            {
                role = UserRole.Admin;
            }
            else { role = UserRole.Authorized; }
            var result = await _userService.SetRoleAsync(id, role, cancellationToken);
            return StatusCode((int)HttpStatusCode.Created, result);
        }

        [HttpPost("Login")]
        [ProducesResponseType(typeof(UserAuthDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] UserAuthDto model, CancellationToken cancellationToken)
        {
            var result = await _userService.LoginAsync(model, cancellationToken);
            return StatusCode((int)HttpStatusCode.OK, result);
        }

        [HttpPost("GetUserInfo")]
        [Authorize]
        public async Task<IActionResult> GetUserInfo(CancellationToken cancellationToken)
        {
            var result = await _userService.GetCurrentUserAsync(cancellationToken);
            if (result == null)
            {
                throw new EntitiesNotFoundException("Пользователь не найден.");
            }
            return StatusCode((int)HttpStatusCode.OK, result);
        }
        [HttpPost("Update orderItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> UpdateAsync([FromBody] UserUpdateInputModel model, CancellationToken cancellationToken)
        {
            await _userService.UpdateAsync(model, cancellationToken);
            return StatusCode((int)HttpStatusCode.OK);
        }
    }
}
