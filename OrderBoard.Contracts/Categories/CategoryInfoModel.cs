using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderBoard.Contracts.Categories
{
    public class CategoryInfoModel
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }

        /// <summary>
        /// Имя категории.
        /// </summary>
        public string Name { get; set; }
    }
}
