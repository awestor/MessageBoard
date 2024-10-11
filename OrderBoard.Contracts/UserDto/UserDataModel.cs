using OrderBoard.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderBoard.Contracts.UserDto
{
    /// <summary>
    /// Модель пользователя
    /// </summary>
    public class UserDataModel
    {
        /// <summary>
        /// Идентефикатор пользователя
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Почта
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// Логин профиля
        /// </summary>
        public string? Login { get; set; }
        /// <summary>
        /// Пароль
        /// </summary>
        public string? Password { get; set; }
        /// <summary>
        /// имя пользователя
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Номер телефона
        /// </summary>
        public string? PhoneNumber { get; set; }
        /// <summary>
        /// Описание профиля
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Дата создания профиля
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Уровень прав доступа
        /// </summary>
        public UserRole Role { get; set; }
    }
}
