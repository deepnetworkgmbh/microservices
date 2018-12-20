using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CorrelationId;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Serilog;
using WebApplicationTemplate.Common;

namespace WebApplicationTemplate.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DependenciesMonitoringController : ControllerBase
    {
        private readonly ICorrelationContextAccessor _correlationContextAccessor;
        private readonly ILogger _log = Log.ForContext<DependenciesMonitoringController>();
        private readonly string _dependencyUrl;

        public DependenciesMonitoringController(
            IOptions<DependenciesConfig> options,
            ICorrelationContextAccessor correlationContextAccessor)
        {
            _correlationContextAccessor = correlationContextAccessor;
            _dependencyUrl = options.Value.DependencyUrl;
        }

        [HttpGet("readiness")]
        public async Task<IActionResult> Readiness()
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.SendAsync(new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"{_dependencyUrl}/monitoring/readiness"),
                    Headers =
                    {
                        {"X-Correlation-Id", _correlationContextAccessor.CorrelationContext.CorrelationId}
                    }
                });
                var statusCode = response.StatusCode;
                if (statusCode == HttpStatusCode.OK)
                {
                    _log.Information("Service is ready");
                    return Ok();
                }

                _log.Warning(
                    "Readiness probe was requested, but {Dependency} responded with {Status} status",
                    _dependencyUrl,
                    statusCode);
                return StatusCode(503);
            }
        }

        [HttpGet("health")]
        public async Task<IActionResult> Health()
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.SendAsync(new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"{_dependencyUrl}/monitoring/health"),
                    Headers =
                    {
                        {"X-Correlation-Id", _correlationContextAccessor.CorrelationContext.CorrelationId}
                    }
                });
                var statusCode = response.StatusCode;
                if (statusCode == HttpStatusCode.OK)
                {
                    _log.Information("Service is healthy");
                    return Ok();
                }

                _log.Warning(
                    "Health probe was requested, but {Dependency} responded with {Status} status",
                    _dependencyUrl,
                    statusCode);
                return StatusCode(503);
            }
        }
    }
}