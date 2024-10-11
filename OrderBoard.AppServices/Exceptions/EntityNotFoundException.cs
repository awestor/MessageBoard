using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderBoard.AppServices.Exceptions
{
    /// <summary>
    /// Ошибка получения сущности
    /// </summary>
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException() : base("Сущность не была найдена.") { }
    }
}
