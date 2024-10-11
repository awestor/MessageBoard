using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderBoard.AppServices.Exceptions
{
    /// <summary>
    /// Генерация ошибки
    /// </summary>
    public class HumanReadableException : Exception
    {
        public string HumanReadableMessage { get; }

        public HumanReadableException(string humanReadableMessage)
        {
            HumanReadableMessage = humanReadableMessage;
        }

        public HumanReadableException(string message, string humanReadableMessage) : base(message)
        {
            HumanReadableMessage = humanReadableMessage;
        }
    }
}
