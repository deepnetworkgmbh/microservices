using Microsoft.AspNetCore.Mvc;
using Serilog;
using WebApplicationTemplate.Common;

namespace WebApplicationTemplate.Controllers
{
    [Route("monitoring")]
    [ApiController]
    public class MonitoringController : ControllerBase
    {
        readonly ILogger log = Log.ForContext("SourceContext", "Monitoring");

        [HttpGet]
        [Route("readiness")]
        public IActionResult Readiness()
        {
            var state = StateManager.CurrentState;
            if (state == State.Healthy)
                return Ok();

            log.Warning("Readiness probe was requested, but service is in {State} state", state);
            return StatusCode(503);
        }

        [HttpGet]
        [Route("health")]
        public IActionResult Health()
        {
            var state = StateManager.CurrentState;
            if (state == State.RequiresRestart)
            {
                log.Warning("Health probe was requested, but service is in {State} state", state);
                return StatusCode(503);
            }

            return Ok();
        }
    }
}
