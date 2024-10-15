using System.ComponentModel.DataAnnotations;

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
        [Required]
        public Guid? UserId { get; set; }
    }
}
