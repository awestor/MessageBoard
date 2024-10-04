using OrderBoard.Domain.Base;

namespace OrderBoard.Domain.Entities
{
    public class EntUser:BaseEntity
    {
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
        /// Список созданных данным пользователем "item"
        /// </summary>
        public virtual List<Item>? Items { get; set; }
        /// <summary>
        /// Список сорершённых заказов пользователем
        /// </summary>
        public virtual List<Order>? History { get; set; }
    }
}
