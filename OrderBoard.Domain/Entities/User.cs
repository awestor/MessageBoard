using OrderBoard.Domain.Base;

namespace OrderBoard.Domain.Entities
{
    public class User:BaseEntity
    {
        /// <summary>
        /// Почта
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Логин профиля
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// имя пользователя
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Номер телефона
        /// </summary>
        public int PhoneNumber { get; set; }
        /// <summary>
        /// Описание профиля
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Дата создания профиля
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}
