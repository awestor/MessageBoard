using MessageBoard.Contracts.Enums;
namespace MessageBoard.Contracts.OrderDTO
{
    internal class OrderDto
    {
        /// <summary>
        /// Идентефикатор заказа
        /// </summary>
        public Guid Id {  get; set; }
        /// <summary>
        /// Описание заказа
        /// </summary>
        public string? Description { get; set; }
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
    }
}
