using Dapr;
using Dapr.Client;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TestPubSubController : ControllerBase
    {
        private readonly ILogger<TestPubSubController> _logger;
        private readonly DaprClient _daprClient;
        public TestPubSubController(ILogger<TestPubSubController> logger, DaprClient daprClient)
        {
            _logger = logger;
            _daprClient = daprClient;
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync()
        {
            Stream stream = Request.Body;
            byte[] buffer = new byte[Request.ContentLength.Value];
            stream.Position = 0L;
            await stream.ReadAsync(buffer, 0, buffer.Length);
            string content = Encoding.UTF8.GetString(buffer);
            _logger.LogInformation("sub" + content);
            return Ok(content);
        }

        [HttpGet("pub")]
        public async Task<ActionResult> PubAsync()
        {
            var data = new WeatherForecast();
            await _daprClient.PublishEventAsync<WeatherForecast>("pubsub", "test_topic", data);
            return Ok();
        }

        const string PUB_SUN = "pubsub";
        const string TOPIC_NAME = "test_topic_code";

        [HttpGet("pubcode")]
        public async Task<ActionResult> PubCodeAsync()
        {
            var data = new WeatherForecast();
            await _daprClient.PublishEventAsync<WeatherForecast>(PUB_SUN, TOPIC_NAME, data);
            return Ok();
        }

        //[Topic(PUB_SUN, TOPIC_NAME)]
        //[HttpPost("subcode")]
        //public async Task<ActionResult> Sub()
        //{
        //    Stream stream = Request.Body;
        //    byte[] buffer = new byte[Request.ContentLength.Value];
        //    stream.Position = 0L;
        //    await stream.ReadAsync(buffer, 0, buffer.Length);
        //    string content = Encoding.UTF8.GetString(buffer);
        //    _logger.LogInformation("testsub" + content);
        //    return Ok(content);
        //}
    }
}
