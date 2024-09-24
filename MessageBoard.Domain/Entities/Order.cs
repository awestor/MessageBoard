using MessageBoard.Contracts.Enums;
using MessageBoard.Domain.Base;

namespace MessageBoard.Domain.Entities
{
    public class Order:BaseEntity
    {
        /// <summary>
        /// Описание заказа
        /// </summary>
        public string? Description {  get; set; }
        /// <summary>
        /// Дата создания заказа
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Количество заказов
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// Итоговая стоимость заказа
        /// </summary>
        public decimal TotalPrice { get; set; }
        /// <summary>
        /// Статус заказа
        /// </summary>
        public OrderStatus OrderStatus { get; set; } = 0;
        /// <summary>
        /// Id пользователя
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// Ссылка на пользователя
        /// </summary>
        public User User { get; set; }
        /// <summary>
        /// Список заказов
        /// </summary>
        public List<OrderItem> OrderList { get; set; }
    }
}
