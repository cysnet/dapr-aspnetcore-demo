using Dapr.Client;
using Dapr.Extensions.Configuration;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Serilog;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                //.ConfigureAppConfiguration(config =>
                //{
                //    var daprClient = new DaprClientBuilder().Build();
                //    var secretDescriptors = new List<DaprSecretDescriptor> { new DaprSecretDescriptor("RabbitMQConnectStr") };
                //    config.AddDaprSecretStore("secrets01", secretDescriptors, daprClient);
                //})
                //.ConfigureAppConfiguration((ht, co) =>
                //{
                //    ht.Configuration = co.Build();
                //})
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>().UseUrls("http://*:5001");//.UseSerilog((c, co) => co.ReadFrom.Configuration(c.Configuration));
                })
            ;
    }
}
