using OrderBoard.AppServices.User.Services;
using Microsoft.AspNetCore.Mvc;
using OrderBoard.Contracts.UserDto;
using System.Net;
using OrderBoard.Contracts.Enums;
using Microsoft.AspNetCore.Authorization;
using OrderBoard.AppServices.Other.Exceptions;
using OrderBoard.Contracts.OrderItem;
using OrderBoard.Contracts.UserDto.Requests;
using Microsoft.EntityFrameworkCore;

namespace OrderBoard.Api.Controllers
{

    [ApiController]
    [Route(template: "[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Create new User")]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] UserCreateModel model, CancellationToken cancellationToken)
        {
            var result = await _userService.CreateAsync(model, cancellationToken);
            return StatusCode((int)HttpStatusCode.Created, result);
        }

        [HttpGet("{id:guid} Get by Id")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(UserInfoModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _userService.GetByIdAsync(id, cancellationToken);
            return Ok(result);
        }

        [HttpPost("{id:guid} Change user role in DB")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SetRole(Guid id, string setRole, CancellationToken cancellationToken)
        { 

            var result = await _userService.SetRoleAsync(id, setRole, cancellationToken);
            return StatusCode((int)HttpStatusCode.Created, result);
        }

        [HttpPost("LoginByLogin")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] UserLoginAuthRequest model, CancellationToken cancellationToken)
        {
            var result = await _userService.LoginAsync(model, null, cancellationToken);
            return StatusCode((int)HttpStatusCode.OK, result);
        }
        [HttpPost("LoginByEmail")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] UserEmailAuthRequest model, CancellationToken cancellationToken)
        {
            var result = await _userService.LoginAsync(null, model, cancellationToken);
            return StatusCode((int)HttpStatusCode.OK, result);
        }

        [HttpPost("GetUserInfo")]
        [Authorize]
        public async Task<IActionResult> GetUserInfo(CancellationToken cancellationToken)
        {
            var result = await _userService.GetCurrentUserAsync(cancellationToken);
            return StatusCode((int)HttpStatusCode.OK, result);
        }
        [HttpPatch("Update User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> UpdateAsync([FromBody] UserUpdateInputModel model, CancellationToken cancellationToken)
        {
            await _userService.UpdateAsync(model, cancellationToken);
            return StatusCode((int)HttpStatusCode.OK);
        }
        [HttpDelete("Delete User if you Auth")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> DeleteAuthAsync(CancellationToken cancellationToken)
        {
            await _userService.DeleteAuthAsync(cancellationToken);
            return StatusCode((int)HttpStatusCode.OK);
        }
        [HttpDelete("{id:guid} Delete User by Id if you have Admin role")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteByIdAsync(Guid? id, CancellationToken cancellationToken)
        {
            await _userService.DeleteByIdAsync(id, cancellationToken);
            return StatusCode((int)HttpStatusCode.OK);
        }
    }
}
