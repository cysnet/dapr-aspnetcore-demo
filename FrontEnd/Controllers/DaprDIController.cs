using Dapr.Client;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrontEnd.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DaprDIController : ControllerBase
    {
        private readonly ILogger<DaprDIController> _logger;
        private readonly DaprClient _daprClient;
        public DaprDIController(ILogger<DaprDIController> logger, DaprClient daprClient)
        {
            _logger = logger;
            _daprClient = daprClient;
        }

        [HttpGet()]
        public async Task<ActionResult> GetAsync()
        {
            var result = await _daprClient.InvokeMethodAsync<IEnumerable<WeatherForecast>>(HttpMethod.Get, "backend", "WeatherForecast");
            return Ok(result);
        }
    }
}
