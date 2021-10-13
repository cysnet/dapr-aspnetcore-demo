﻿using Dapr.Client;

using GrpcGreeter;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrontEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DaprController : ControllerBase
    {

        private readonly ILogger<DaprController> _logger;

        public DaprController(ILogger<DaprController> logger)
        {
            _logger = logger;
        }

        // 通过HttpClient调用BackEnd
        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            using var httpClient = DaprClient.CreateInvokeHttpClient();
            var result = await httpClient.GetAsync("http://backend/WeatherForecast");
            var resultContent = string.Format("result is {0} {1}", result.StatusCode, await result.Content.ReadAsStringAsync());
            return Ok(resultContent);
        }

        // 通过DaprClient调用BackEnd
        [HttpGet("get2")]
        public async Task<ActionResult> Get2Async()
        {
            using var daprClient = new DaprClientBuilder().Build();
            var result = await daprClient.InvokeMethodAsync<IEnumerable<WeatherForecast>>(HttpMethod.Get, "backend", "WeatherForecast");
            return Ok(result);
        }

        [HttpGet("ip")]
        public async Task<ActionResult> GetIpAsync()
        {
            using var _daprClient = new DaprClientBuilder().Build();
            var result = await _daprClient.InvokeMethodAsync<List<string>>(HttpMethod.Get, "backend", "api/Ip");
            return Ok(result);
        }


        [HttpGet("grpc")]
        public async Task<ActionResult> GrpcAsync()
        {
            using var daprClient = new DaprClientBuilder().Build();
            var result = await daprClient.InvokeMethodGrpcAsync<HelloRequest, HelloReply>("backend", "sayhi", new HelloRequest { Name = "aaa" });
            return Ok(result);
        }

    }
}
