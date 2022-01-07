using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace DeleteProductServiceQueue
{
    public static class ConsumeQueueMessage
    {
        [FunctionName("ConsumeQueueMessage")]
        public static void Run([ServiceBusTrigger("az-deleteproduct-queue", Connection = "")] string myQueueItem, FunctionContext context)
        {
            var logger = context.GetLogger("Function1");
            logger.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        }
    }
}
