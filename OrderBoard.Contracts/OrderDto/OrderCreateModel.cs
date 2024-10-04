using OrderBoard.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderBoard.Contracts.OrderDto
{
    public class OrderCreateModel
    {
        /// <summary>
        /// Id пользователя
        /// </summary>
        public Guid UserId { get; set; }
    }
}
