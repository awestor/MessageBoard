using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderBoard.Contracts.ErrorDto
{
    public class ApiError(string message, string description, string traceID, int code)
    {
        /// <summary>
        /// Сообщение об ошибке
        /// </summary>
        public string Message { get; set; } = message;

        /// <summary>
        /// Описание ошибки
        /// </summary>
        public string Description { get; set; } = description;

        /// <summary>
        /// Идентификатор запроса
        /// </summary>
        public string TraceID { get; set; } = traceID;

        /// <summary>
        /// Код ошбки
        /// </summary>
        public int Code { get; set; } = code;
    }
}
