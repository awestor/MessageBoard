using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderBoard.Contracts.Categories
{
    /// <summary>
    /// Информационная модель категории
    /// </summary>
    public class CategoryInfoModel
    {
        /// <summary>
        /// Идентефикатор категории, хранящейся в базе
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Дата создания категории, хранящейся в базе
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Имя категории.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Описание категории.
        /// </summary>
        public string Description { get; set; }
    }
}
