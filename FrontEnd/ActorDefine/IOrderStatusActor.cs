using Dapr.Actors;

using System.Threading.Tasks;

namespace FrontEnd.ActorDefine
{
    public interface IOrderStatusActor : IActor
    {
        Task<string> Paid(string orderId);
        Task<string> GetStatus(string orderId);
        Task StartTimerAsync(string name, string text);
    }
}
