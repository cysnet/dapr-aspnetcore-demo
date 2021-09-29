using Dapr.Actors;
using Dapr.Actors.Client;

using FrontEnd.ActorDefine;

using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

namespace FrontEnd.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly IActorProxyFactory _actorProxyFactory;

        public ActorController(IActorProxyFactory actorProxyFactory)
        {
            _actorProxyFactory = actorProxyFactory;
        }

        [HttpGet("paid/{orderId}")]
        public async Task<ActionResult> PaidAsync(string orderId)
        {
            var actorId = new ActorId("myid-" + orderId);
            var proxy = ActorProxy.Create<IOrderStatusActor>(actorId, "OrderStatusActor");
            var result = await proxy.Paid(orderId);
            return Ok(result);
        }

        [HttpGet("get/{orderId}")]
        public async Task<ActionResult> GetAsync(string orderId)
        {
            var proxy = _actorProxyFactory.CreateActorProxy<IOrderStatusActor>(
                new ActorId("myid-" + orderId),
                "OrderStatusActor");

            return Ok(await proxy.GetStatus(orderId));
        }
    }
}
