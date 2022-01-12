using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

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

                return apiResponse;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}
