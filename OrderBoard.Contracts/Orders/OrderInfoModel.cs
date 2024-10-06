using OrderBoard.Contracts.Enums;

namespace OrderBoard.Contracts.Orders
{
    public class OrderInfoModel
    {
        /// <summary>
        /// Дата создания заказа
        /// </summary>
        public DateTime PaidAt { get; set; }
        /// <summary>
        /// Дата создания заказа
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Количество заказов
        /// </summary>
        public decimal TotalCount { get; set; }
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
    }
}
