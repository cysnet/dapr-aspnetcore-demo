using Dapr.Client;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrontEnd.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SecretsController : ControllerBase
    {
        private readonly ILogger<SecretsController> _logger;
        private readonly DaprClient _daprClient;
        private readonly IConfiguration _configuration;
        public SecretsController(ILogger<SecretsController> logger, DaprClient daprClient, IConfiguration configuration)
        {
            _logger = logger;
            _daprClient = daprClient;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            Dictionary<string, string> secrets = await _daprClient.GetSecretAsync("secrets01", "RabbitMQConnectStr");
            return Ok(secrets["RabbitMQConnectStr"]);
        }

        [HttpGet("get01")]
        public async Task<ActionResult> Get01Async()
        {
            return Ok(_configuration["RabbitMQConnectStr"]);
        }
    }
}
