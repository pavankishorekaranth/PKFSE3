using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;

namespace TestQueueTrigger
{
    public static class ConsumeMessage
    {
        [Function("ConsumeMessage")]
        public static void Run([ServiceBusTrigger("az-deleteproduct-queue", Connection = "")] Message myQueueItem, FunctionContext context)
        {
            var logger = context.GetLogger("Function1");
            logger.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        }
    }
}
