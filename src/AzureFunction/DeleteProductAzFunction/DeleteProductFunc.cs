using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.ServiceBus;
using System.IO;
using System.Text;
using Microsoft.Azure.ServiceBus;

namespace DeleteProductAzFunction
{
    public static class DeleteProductFunc
    {
        [FunctionName("DeleteProductFunc")]
        public static async Task<string> Run([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "DeleteProductFunc/{productId}")] HttpRequestData req,
            FunctionContext executionContext, string productId)
        {
            var logger = executionContext.GetLogger("DeleteProduct");
            logger.LogInformation("C# HTTP trigger function processed a request.");

            string apiResponse = "";
            try
            {
                HttpClient httpClient = new();
                httpClient.BaseAddress = new Uri("https://localhost:44396/gateway/");
                httpClient.Timeout = new TimeSpan(0, 2, 0);

                using (var responseData = await httpClient.DeleteAsync("DeleteProduct/" + productId))
                {
                    apiResponse = await responseData.Content.ReadAsStringAsync();
                }

                if (apiResponse == "Product is deleted successfully")
                {
                    string queueName = "az-deleteproduct-queue";
                    string serviceBusConnectionString = "Endpoint=sb://pktestsb.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=FXBNZ+rAkXFbt9r/Evg456NrWVXEXvkrLI7ilwn/OKY=";
                    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                    var client = new QueueClient(serviceBusConnectionString, queueName);
                    
                    var message = new Message(Encoding.UTF8.GetBytes(requestBody));
                    await client.SendAsync(message);
                }

                return "Function Executed Successfully";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}
