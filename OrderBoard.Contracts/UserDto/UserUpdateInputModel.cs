using OrderBoard.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderBoard.Contracts.UserDto
{
    public class UserUpdateInputModel
    {
        /// <summary>
        /// имя пользователя
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Логин профиля
        /// </summary>
        public string? Login { get; set; }
        /// <summary>
        /// Описание профиля
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Номер телефона
        /// </summary>
        public string? PhoneNumber { get; set; }
        /// <summary>
        /// Пароль
        /// </summary>
        public string? Password { get; set; }
        /// <summary>
        /// Почта
        /// </summary>
        public string? Email { get; set; }
    }
}
