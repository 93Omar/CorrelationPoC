using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Shared
{
    public class CorrelationMiddleware
    {
        private readonly ILogger<CorrelationMiddleware> _logger;
        private readonly RequestDelegate _next;
        private const string correlationIdValue = Correlation.HeaderName;

        public CorrelationMiddleware(RequestDelegate next, ILogger<CorrelationMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var correlationId = context.Request.Headers[correlationIdValue].FirstOrDefault();

            if (string.IsNullOrEmpty(correlationId))
                correlationId = Guid.NewGuid().ToString();

            context.Items[Correlation.ContextName] = correlationId;

            context.Response.OnStarting(state =>
            {
                var httpContext = (HttpContext)state;

                httpContext.Response.Headers.Append(correlationIdValue, correlationId);

                return Task.CompletedTask;
            }, context);

            using (_logger.BeginScope("CorrelationId: {CorrelationId}", correlationId))
            {
                await _next(context);
            }
        }
    }
}
