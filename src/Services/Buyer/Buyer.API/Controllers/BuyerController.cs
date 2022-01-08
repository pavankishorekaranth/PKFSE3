using Buyer.Application.Commands;
using Buyer.Application.Exceptions;
using Buyer.Application.Queries;
using Buyer.Application.ViewModel;
using EventBus.Message.Events;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;



namespace Buyer.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BuyerController : ControllerBase
    {
        private readonly ILogger<BuyerController> _logger;
        private readonly IMediator _mediator;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public BuyerController(ILogger<BuyerController> logger, IMediator mediator, IPublishEndpoint publishEndpoint, HttpClient httpClient, IConfiguration configuration, ISendEndpointProvider sendEndpointProvider)
        {
            _logger = logger;
            _mediator = mediator;
            _publishEndpoint = publishEndpoint;
            _httpClient = httpClient;
            _configuration = configuration;
            _sendEndpointProvider = sendEndpointProvider;
        }

        [HttpPost("PlaceBid")]
        public async Task<ActionResult> PlaceBid([FromBody] CreateBidCommand bid)
        {
            try
            {
                _httpClient.BaseAddress = new Uri(_configuration.GetValue<string>("GatewaySettings:BaseUrl"));
                _httpClient.Timeout = new TimeSpan(0, 2, 0);

                var response = await _httpClient.GetAsync("GetAllProducts");
                var apiResponse = await response.Content.ReadAsStringAsync();
                List<ProductDetails> products = JsonConvert.DeserializeObject<List<ProductDetails>>(apiResponse);

                if(products.Any(x => x.Id.Contains(bid.ProductId)))
                {
                    var product = products.Where(x => x.Id.Contains(bid.ProductId)).FirstOrDefault();
                    if (DateTime.UtcNow > product.BidEndDate)
                    {
                        throw new BidEndDateExpiredException("You cannot place Bid after Bid End Date");
                    }

                    _logger.LogInformation("Creating Bid");
                    var result = await _mediator.Send(bid);
                    bool useRabbitMq = _configuration.GetValue<bool>("BaseServiceSettings:UserabbitMq");
                    if (useRabbitMq)
                    {
                        await _publishEndpoint.Publish<CreateBidEvent>(result);
                    }
                    else
                    {
                        var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri(_configuration.GetValue<string>("AzureServiceBusQueueBusSettings:CreateBidQueueSendEndPoint")));
                        await sendEndpoint.Send<CreateBidEvent>(result);
                    }

                    _logger.LogInformation($"Bid is created with Id {result.Id}");
                    return Ok("Bid created successfully");
                }
                else
                {
                    throw new NotFoundException("Product is not found");
                }
            }
            catch(BidAlreadyPlacedByUserException ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(ex.Message);
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex.Message);
                var items = ex.Errors.SelectMany(d => d.Value).ToList();
                return BadRequest(string.Join(";", items));
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500);
            }
        }


        [HttpPut("UpdateBidAmount/{id}")]
        public async Task<ActionResult<Unit>> UpdateBidAmount(string id, UpdateBidCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("Bid Id's dont match");
            }

            try
            {
                _httpClient.BaseAddress = new Uri(_configuration.GetValue<string>("GatewaySettings:BaseUrl"));
                _httpClient.Timeout = new TimeSpan(0, 2, 0);

                var response = await _httpClient.GetAsync("GetAllProducts");
                var apiResponse = await response.Content.ReadAsStringAsync();
                List<ProductDetails> products = JsonConvert.DeserializeObject<List<ProductDetails>>(apiResponse);

                if (products.Any(x => x.Id.Contains(command.ProductId)))
                {
                    var product = products.Where(x => x.Id.Contains(command.ProductId)).FirstOrDefault();
                    if (DateTime.UtcNow > product.BidEndDate)
                    {
                        throw new BidEndDateExpiredException("You cannot place Bid after Bid End Date");
                    }

                    _logger.LogInformation("Updating Bid");
                    var result = await _mediator.Send(command);
                    _logger.LogInformation($"Bid is updated");

                    //Get updated Bid Info
                    var bidResult = await _mediator.Send(new GetBidQuery(id));
                    bool useRabbitMq = _configuration.GetValue<bool>("BaseServiceSettings:UserabbitMq");
                    if (useRabbitMq)
                    {
                        await _publishEndpoint.Publish<UpdateBidEvent>(bidResult);
                    }
                    else
                    {
                        var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri(_configuration.GetValue<string>("AzureServiceBusQueueBusSettings:UpdateBidQueueSendEndPoint")));
                        await sendEndpoint.Send<UpdateBidEvent>(bidResult);
                    }

                    return Ok("Bid amount updated successfully");
                }
                else
                {
                    throw new NotFoundException("Product is not found");
                }
            }
            catch(NotFoundException ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500);
            }
        }
    }
}
