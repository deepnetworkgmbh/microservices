using System.Threading.Tasks;
using CorrelationId;
using Microsoft.AspNetCore.Http;
using Serilog.Context;

namespace WebApplicationTemplate.Common
{
    public class CorrelationIdLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public CorrelationIdLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ICorrelationContextAccessor correlationContextAccessor)
        {
            var correlationId = correlationContextAccessor.CorrelationContext.CorrelationId;

            using (LogContext.PushProperty("XCorrelationId", correlationId))
            {
                await _next(context);
            }
        }
    }
}