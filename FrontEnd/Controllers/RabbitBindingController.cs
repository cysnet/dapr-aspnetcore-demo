using Dapr.Client;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RabbitBindingController : ControllerBase
    {
        private readonly ILogger<RabbitBindingController> _logger;
        public RabbitBindingController(ILogger<RabbitBindingController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public ActionResult Post()
        {
            Stream stream = Request.Body;
            byte[] buffer = new byte[Request.ContentLength.Value];
            stream.Position = 0L;
            stream.ReadAsync(buffer, 0, buffer.Length);
            string content = Encoding.UTF8.GetString(buffer);
            _logger.LogInformation(".............binding............." + content);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> GetAsync([FromServices]DaprClient daprClient)
        {
            await daprClient.InvokeBindingAsync("RabbitBinding", "create", "9999999");
            return Ok();
        }
    }
}
