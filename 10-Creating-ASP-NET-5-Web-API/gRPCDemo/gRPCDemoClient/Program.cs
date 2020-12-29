using Grpc.Core;
using Grpc.Net.Client;
using gRPCDemoService;
using System;
using System.Threading.Tasks;

namespace gRPCDemoClient
{
    class Program
    {
static async Task Main(string[] args)
{
    var channel = GrpcChannel.ForAddress("https://localhost:5001");

    var client = new Greeter.GreeterClient(channel);
    HelloReply response = await client.SayHelloAsync(new HelloRequest { Name="Suneel" });
    Console.WriteLine(response.Message);
}
    }
}
