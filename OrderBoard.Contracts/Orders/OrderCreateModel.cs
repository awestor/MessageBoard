namespace OrderBoard.Contracts.Orders
{
    /// <summary>
    /// Модель создания заказа
    /// </summary>
    public class OrderCreateModel
    {
        /// <summary>
        /// Id пользователя
        /// </summary>
        public Guid UserId { get; set; }
    }
}
