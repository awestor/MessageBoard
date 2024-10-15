using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderBoard.Contracts.UserDto.Requests
{
    public class UserEmailAuthRequest
    {
        /// <summary>
        /// Почта
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// Пароль
        /// </summary>
        public required string? Password { get; set; }
    }
}
