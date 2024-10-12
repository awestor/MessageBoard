using OrderBoard.Contracts.Enums;
using OrderBoard.Domain.Base;

namespace OrderBoard.Domain.Entities
{
    /// <summary>
    /// Сущность заказа
    /// </summary>
    public class Order:BaseEntity
    {
        /// <summary>
        /// Дата создания заказа
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Дата создания заказа
        /// </summary>
        public DateTime PaidAt { get; set; }
        /// <summary>
        /// Количество заказов
        /// </summary>
        public decimal? TotalCount { get; set; }
        /// <summary>
        /// Итоговая стоимость заказа
        /// </summary>
        public decimal? TotalPrice { get; set; }
        /// <summary>
        /// Статус заказа
        /// </summary>
        public OrderStatus? OrderStatus { get; set; } = 0;
        /// <summary>
        /// Id пользователя
        /// </summary>
        public Guid? UserId { get; set; }
        /// <summary>
        /// Ссылка на пользователя
        /// </summary>
        public EntUser? User { get; set; }
        /// <summary>
        /// Список заказов
        /// </summary>
        public List<OrderItem>? OrderList { get; set; }
    }
}
