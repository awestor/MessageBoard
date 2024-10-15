using OrderBoard.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderBoard.Contracts.UserDto
{
    /// <summary>
    /// Информационная модель пользователя
    /// </summary>
    public class UserInfoModel
    {
        /// <summary>
        /// Идентефикатор пользователя
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Логин профиля
        /// </summary>
        public string? Login { get; set; }
        /// <summary>
        /// Описание профиля
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Уровень прав доступа
        /// </summary>
        public UserRole Role { get; set; }
        /// <summary>
        /// Дата создания профиля
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}
