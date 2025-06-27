using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class CorrelationDelegatingHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CorrelationDelegatingHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string? correlationId = _httpContextAccessor.HttpContext?.Items[Correlation.ContextName] as string;

            if (!request.Headers.Contains(Correlation.HeaderName) && correlationId is not null)
                request.Headers.Add(Correlation.HeaderName, correlationId);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
