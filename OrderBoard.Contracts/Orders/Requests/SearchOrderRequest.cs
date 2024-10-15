using OrderBoard.Contracts.BasePagination;
using OrderBoard.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderBoard.Contracts.Orders.Requests
{
    public class SearchOrderRequest : BasePaginationRequest
    {
        /// <summary>
        /// Id пользователя по которому осуществляется поиск
        /// </summary>
        public Guid? UserId { get; set; }

        /// <summary>
        /// Минимальный cтатус заказа для отображения
        /// </summary>
        public OrderStatus? MinOrderStatus { get; set; }
        /// <summary>
        /// Максимальный cтатус заказа для отображения
        /// </summary>
        public OrderStatus? MaxOrderStatus { get; set; }
    }
}
