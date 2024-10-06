using OrderBoard.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderBoard.Contracts.Orders
{
    public class OrderDataModel
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
