using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderBoard.Contracts.ErrorDto
{
    public class HumanReadableError(string message, string description, string traceID, int code, string humanReadableErrorMessage) : ApiError(message, description, traceID, code)
    {
        /// <summary>
        /// Человеко-читаемое сообщение об ошибке для пользователя.
        /// </summary>
        public string HumanReadableErrorMessage { get; } = humanReadableErrorMessage;
    }
}
