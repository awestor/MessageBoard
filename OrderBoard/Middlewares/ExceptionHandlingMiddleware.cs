using static System.Net.Mime.MediaTypeNames;
using System.Net;
using System.Text.Json;
using OrderBoard.Contracts.ErrorDto;
using OrderBoard.AppServices.Other.Exceptions;

namespace OrderBoard.Api.Middlewares
{
    public class ExceptionHandlingMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next ?? throw new ArgumentNullException(nameof(next));

        /// <summary>
        /// Вызывается для обработки исключений во время работы приложения.
        /// </summary>
        /// <param name="context">Контекст данных HTTP-запроса.</param>
        /// <param name="environment">Информация о среде окружения приложения.</param>
        /// <param name="serviceProvider">Объект предоставляющий пользовательскую поддержку другим объектам.</param>
        /// <returns>Задача, представляющая собой завершение обработки запроса.</returns>
        public async Task Invoke(HttpContext context, IHostEnvironment environment, IServiceProvider serviceProvider)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = GetStatusCode(exception);

                var apiError = CreateApiError(exception, context, environment);
                await context.Response.WriteAsync(JsonSerializer.Serialize(apiError, new JsonSerializerOptions
                {
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(System.Text.Unicode.UnicodeRanges.All),
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }));
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
                EntitysNotVaildException nv => new HumanReadableError("Ошибка.", null!, traceID, statusCode, nv.HumanReadableMessage),
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
                EntitysNotVaildException => HttpStatusCode.NotAcceptable,
                _ => HttpStatusCode.InternalServerError
            };

            return (int)statusCode;
        }
    }
}
