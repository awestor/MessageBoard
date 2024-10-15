using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderBoard.Contracts.OrderItem
{
    public class OrderItemUpdateModel
    {
        /// <summary>
        /// Идентефикатор поля заказа
        /// </summary>
        public Guid? Id { get; set; }
        /// <summary>
        /// Количество товаров
        /// </summary>
        public decimal? Count { get; set; }
    }
}
