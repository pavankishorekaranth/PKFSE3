using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace DeleteProductFunction
{
    public static class DeleteProductFunc
    {
        [Function("DeleteProduct")]
        [return: ServiceBus("az-deleteproduct-queue", Microsoft.Azure.WebJobs.ServiceBus.ServiceBusEntityType.Queue)]
        public async static Task<string> Run([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route ="DeleteProduct/{productId}")] HttpRequestData req,
            FunctionContext executionContext, string productId)
        {
            var logger = executionContext.GetLogger("Function1");
            logger.LogInformation("C# HTTP trigger function processed a request.");

            string apiResponse = "";
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(Environment.GetEnvironmentVariable("GatewayUrl"));
                httpClient.Timeout = new TimeSpan(0, 2, 0);

                using (var responseData = await httpClient.DeleteAsync("DeleteProduct/" + productId))
                {
                    apiResponse = await responseData.Content.ReadAsStringAsync();
                }

                //if (apiResponse == "Product is deleted successfully")
                //{
                //    string queueName = Environment.GetEnvironmentVariable("QueueName");
                //    string serviceBusConnectionString = Environment.GetEnvironmentVariable("ServiceBusConnString");

                //    string messageBody = apiResponse;
                //    var client = new QueueClient(serviceBusConnectionString, queueName);
                //    Message message = new Message(Encoding.UTF8.GetBytes(messageBody));
                //    await client.SendAsync(message);
                //}

                return apiResponse;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}
