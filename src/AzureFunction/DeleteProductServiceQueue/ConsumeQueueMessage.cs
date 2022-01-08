using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace DeleteProductServiceQueue
{
    public static class ConsumeQueueMessage
    {
        [Function("ConsumeQueueMessage")]
        public static void Run([ServiceBusTrigger("az-deleteproduct-queue", Connection = "Endpoint=sb://pktestsb.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=FXBNZ+rAkXFbt9r/Evg456NrWVXEXvkrLI7ilwn/OKY=")] Message message, FunctionContext context)
        {
            var logger = context.GetLogger("Function1");
            logger.LogInformation($"C# ServiceBus queue trigger function processed message: {message}");
        }
    }
}
