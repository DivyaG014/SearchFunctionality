using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights;
using NLog;
using Microsoft.ApplicationInsights.Extensibility.Implementation;

namespace SearchFunctionality.Middleware
{
    public class RequestMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly NLog.ILogger _logger = LogManager.GetCurrentClassLogger();

        public RequestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, TelemetryClient telemetryClient)
        {
            using (telemetryClient.StartOperation<RequestTelemetry>("HAVI.WMO.WebApi." + context.Request.Path))
            {
                var originalResponseBody = context.Response.Body;
                using var outputBuffer = new MemoryStream();
                context.Response.Body = outputBuffer;
                await BeforeAction(context);
                await _next(context);
                outputBuffer.Seek(0, SeekOrigin.Begin);
                await AfterRequest(context, outputBuffer);
                await outputBuffer.CopyToAsync(originalResponseBody);
            }
        }

        private async Task AfterRequest(HttpContext context, MemoryStream buffer)
        {
            _logger.Info("Response: {status}", context.Response.StatusCode);
            if (_logger.IsTraceEnabled)
            {
                var body = await new StreamReader(buffer).ReadToEndAsync();
                buffer.Seek(0, SeekOrigin.Begin);

                _logger.Trace("Response body: {body}", body);
            }
        }

        private async Task BeforeAction(HttpContext context)
        {
            context.Request.EnableBuffering();
            string body = "";
            if (context.Request.Body != null)
            {
                body = await new StreamReader(context.Request.Body).ReadToEndAsync();
                context.Request.Body.Seek(0, SeekOrigin.Begin);
            }

            _logger.Info("Request: {route} with parameters '{params}' and '{body}'", context.Request.Path, context.Request.QueryString, body);            
        }

    }
}
