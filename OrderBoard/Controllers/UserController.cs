﻿using OrderBoard.AppServices.User.Services;
using Microsoft.AspNetCore.Mvc;
using OrderBoard.Contracts.UserDto;
using System.Net;

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
    }
}
