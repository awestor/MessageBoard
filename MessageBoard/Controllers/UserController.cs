using MessageBoard.AppServices.User.Services;
using Microsoft.AspNetCore.Mvc;

namespace MessageBoard.Api.Controllers
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
    }
}
