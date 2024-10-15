using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderBoard.Contracts.Categories
{
    public class CategoryDataModel
    {
        /// <summary>
        /// Идентефикатор категории, хранящейся в базе
        /// </summary>
        public Guid? Id { get; set; }
        /// <summary>
        /// Идентификатор родителя
        /// </summary>
        public Guid? ParentId { get; set; }
        /// <summary>
        /// Название
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        public string? Description { get; set; }
    }
}
