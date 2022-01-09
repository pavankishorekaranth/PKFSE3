using System;
using System.Text;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DeleteProductServiceQueueTrigger
{
    public static class ConsumeMessage
    {
        [Function("ConsumeMessage")]
        public static void Run([ServiceBusTrigger("az-deleteproduct-queue", Connection = "sbConnString")] string message, FunctionContext context)
        {
            var logger = context.GetLogger("ConsumeMessage");
            logger.LogInformation($"C# ServiceBus queue trigger function processed message: {message}");
        }
    }
}
