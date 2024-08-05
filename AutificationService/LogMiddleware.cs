using System.Diagnostics.Tracing;
using System;
using System.Collections.Generic;
using System.IO;

namespace AutificationService
{
    public class LogMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        public LogMiddleware(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            _logger.WriteEvent("Я твой Middleware");
            await _next(httpContext);
        }
    }
}
