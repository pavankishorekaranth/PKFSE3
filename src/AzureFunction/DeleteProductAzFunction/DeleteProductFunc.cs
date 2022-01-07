using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Worker.Extensions;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.ServiceBus;

namespace DeleteProductAzFunction
{
    public static class DeleteProductFunc
    {
        [Function("DeleteProductFunc")]
        //[return: ServiceBus("az-deleteproduct-queue", ServiceBusEntityType.Queue)]
        public async static Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "DeleteProductFunc/{productId}")] HttpRequestData req,
            FunctionContext executionContext, string productId)
        {
            var logger = executionContext.GetLogger("DeleteProduct");
            logger.LogInformation("C# HTTP trigger function processed a request.");
            
            string apiResponse = "";
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri("https://localhost:44396/gateway/");
                httpClient.Timeout = new TimeSpan(0, 2, 0);

                using (var responseData = await httpClient.DeleteAsync("DeleteProduct/"+ productId))
                {
                    apiResponse = await responseData.Content.ReadAsStringAsync();
                }

                var response = req.CreateResponse(HttpStatusCode.OK);
                response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
                response.WriteString(apiResponse);

                return response;
            }
            catch(Exception ex)
            {
                var response = req.CreateResponse(HttpStatusCode.BadRequest);
                response.WriteString(ex.ToString());
                return response;
            }
        }
    }
}
