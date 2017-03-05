using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace myapi.Middleware
{
    public class CorrelationIdResponseMiddleware
    {
        private const string CorrelationIdHeaderName = "X-Correlation-Id";
        private readonly RequestDelegate _next;

        public CorrelationIdResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            context
                .Response
                .Headers
                .Add(CorrelationIdHeaderName, context.TraceIdentifier);

            return _next(context);
        }
    }
}