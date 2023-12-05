using System.Text;

namespace MainTz.RestApi.BLL.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;
        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            string bodyContent = "";

            using(var reader = new StreamReader(context.Request.Body))
            {
                bodyContent = await reader.ReadToEndAsync();
                context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(bodyContent));
            }

            _logger.LogDebug($"[{context.Request.Path}]" +
                $" [{context.Request.Method}]" +
                $" [{context.Request.Headers["Authorization"]}]" +
                $" [{bodyContent}]");

            _logger.LogTrace($"[{context.Request.Path}]" +
                $" [{context.Request.Method}]" +
                $" [{context.Request.BodyReader}]" +
                $" [{bodyContent}]");

            await _next.Invoke(context);
        }
    }
}