using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplicationTemplate.Common;

namespace WebApplicationTemplate.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MonitoringController : ControllerBase
    {
        private readonly ILogger<MonitoringController> _log;

        public MonitoringController(ILogger<MonitoringController> log)
        {
            _log = log;
        }

        [HttpGet("readiness")]
        public IActionResult Readiness()
        {
            var state = StateManager.CurrentState;
            if (state == State.Healthy)
            {
                _log.LogInformation("Service is ready");
                return Ok();
            }

            _log.LogWarning("Readiness probe was requested, but service is in {State} state", state);
            return StatusCode(503);
        }

        [HttpGet("health")]
        public IActionResult Health()
        {
            var state = StateManager.CurrentState;
            if (state == State.RequiresRestart)
            {
                _log.LogWarning("Health probe was requested, but service is in {State} state", state);
                return StatusCode(503);
            }

            _log.LogInformation("Service is healthy");
            return Ok();
        }
    }
}