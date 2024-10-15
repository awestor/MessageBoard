using static System.Net.Mime.MediaTypeNames;
using System.Net;
using System.Text.Json;
using OrderBoard.Contracts.ErrorDto;
using OrderBoard.AppServices.Other.Exceptions;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http;
using Serilog.Context;

namespace OrderBoard.Api.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private static readonly JsonSerializerOptions JsonSerializerOptions = new()
        {
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(System.Text.Unicode.UnicodeRanges.All),
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        private const string LogTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode}";
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }
        /// <summary>
        /// Вызывается для обработки исключений во время работы приложения.
        /// </summary>
        /// <param name="context">Контекст данных HTTP-запроса.</param>
        /// <param name="environment">Информация о среде окружения приложения.</param>
        /// <param name="serviceProvider">Объект предоставляющий пользовательскую поддержку другим объектам.</param>
        /// <returns>Задача, представляющая собой завершение обработки запроса.</returns>
        public async Task Invoke(HttpContext context
            , IHostEnvironment environment
            , IServiceProvider serviceProvider
            , ILogger<ExceptionHandlingMiddleware> logger)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                var statusCode = GetStatusCode(exception);

                using (LogContext.PushProperty("Request.TraceId", context.TraceIdentifier))
                using (LogContext.PushProperty("Request.UserName", context.User.Identity?.Name ?? string.Empty))
                using (LogContext.PushProperty("Request.Connection", context.Connection.RemoteIpAddress?.ToString() ?? string.Empty))
                using (LogContext.PushProperty("Request.DisplayUrl", context.Request.GetDisplayUrl()))
                {
                    logger.LogError(exception, LogTemplate,
                    context.Request.Method,
                        context.Request.Path.ToString(),
                        (int)statusCode);
                }

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = ((int)statusCode);

                var apiError = CreateApiError(exception, context, environment);
                
                await context.Response.WriteAsync(JsonSerializer.Serialize(apiError, JsonSerializerOptions));
            }
        }

        private static ApiError CreateApiError(Exception exception, HttpContext context, IHostEnvironment environment)
        {
            var traceID = context.TraceIdentifier;
            var statusCode = GetStatusCode(exception);

            if (environment.IsDevelopment())
            {
                return new ApiError(exception.Message, exception.StackTrace ?? string.Empty, traceID, statusCode);
            }

            return exception switch
            {
                EntititysNotVaildException nv => new HumanReadableError("Введённые параметры вне допустимых значений.", null!, traceID, statusCode, nv.HumanReadableMessage),
                EntitiesNotFoundException ex => new HumanReadableError("Ошибка.", null!, traceID, statusCode, ex.HumanReadableMessage),
                EntityNotFoundException => new ApiError("Сущность не найдена.", null!, traceID, statusCode),
                _ => new ApiError("Произошла непредвиденная ошибка.", null!, traceID, statusCode)
            };
        }

        private static int GetStatusCode(Exception exception)
        {
            var statusCode = exception switch
            {
                ArgumentException => HttpStatusCode.BadRequest,
                EntityNotFoundException => HttpStatusCode.NotFound,
                EntitiesNotFoundException => HttpStatusCode.NotFound,
                EntititysNotVaildException => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError
            };

            return (int)statusCode;
        }
    }
}
