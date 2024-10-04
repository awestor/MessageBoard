using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderBoard.Contracts.Items
{
    public class ItemCreateModel
    {
        /// <summary>
        /// Имя товара
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Количество товара
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// Коментарий к товару
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Коментарий к товару
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Коментарий к товару
        /// </summary>
        public string? Comment { get; set; }
        /// <summary>
        /// Id пользователя что создал его
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// Id категории за которой закреплён товар
        /// </summary>
        public Guid CategoryId { get; set; }
    }
}
