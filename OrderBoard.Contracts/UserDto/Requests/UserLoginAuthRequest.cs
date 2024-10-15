using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderBoard.Contracts.UserDto.Requests
{
    public class UserLoginAuthRequest
    {
        /// <summary>
        /// Логин профиля
        /// </summary>
        public string? Login { get; set; }
        /// <summary>
        /// Пароль
        /// </summary>
        public required string? Password { get; set; }
    }
}
