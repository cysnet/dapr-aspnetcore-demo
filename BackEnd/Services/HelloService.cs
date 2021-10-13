using Dapr.AppCallback.Autogen.Grpc.v1;
using Dapr.Client.Autogen.Grpc.v1;

using Google.Protobuf.WellKnownTypes;

using Grpc.Core;

using GrpcGreeter;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Services
{
    public class HelloService : AppCallback.AppCallbackBase
    {
        public override async Task<InvokeResponse> OnInvoke(InvokeRequest request, ServerCallContext context)
        {
            var response = new InvokeResponse();
            switch (request.Method)
            {
                case "sayhi":
                    var input = request.Data.Unpack<HelloRequest>();
                    response.Data = Any.Pack(new HelloReply { Message = "ok" });
                    break;
            }
            return response;
        }
    }

}
