using NLog;
using System.Net;
using System.Text.Json;
using ILogger = NLog.ILogger;

namespace SearchFunctionality.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private ILogger _logger = LogManager.GetCurrentClassLogger();
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                _logger.Error(error);
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
#if DEBUG
                var result = JsonSerializer.Serialize(new { message = error?.ToString() });
                await response.WriteAsync(result);
#endif
            }
        }
    }
}
