namespace OrderBoard.Contracts.Enums
{
    /// <summary>
    /// Статус заказа
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// Неопределен
        /// </summary>
        Undefind = 0,
        /// <summary>
        /// Черновик
        /// </summary>
        Draft = 1,
        /// <summary>
        /// Опубликован
        /// </summary>
        Ordered = 2
    }
}
