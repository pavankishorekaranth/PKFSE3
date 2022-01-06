using Buyer.Application.Commands;
using Buyer.Application.Exceptions;
using Buyer.Application.Queries;
using Buyer.Application.ViewModel;
using EventBus.Message.Events;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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

        public BuyerController(ILogger<BuyerController> logger, IMediator mediator, IPublishEndpoint publishEndpoint, HttpClient httpClient)
        {
            _logger = logger;
            _mediator = mediator;
            _publishEndpoint = publishEndpoint;
            _httpClient = httpClient;
        }

        [HttpPost("place-bid")]
        public async Task<ActionResult> PlaceBid([FromBody] CreateBidCommand bid)
        {
            try
            {
                _httpClient.BaseAddress = new Uri("http://localhost:5000/");
                _httpClient.Timeout = new TimeSpan(0, 2, 0);

                var response = await _httpClient.GetAsync("api/v1/Seller/GetAllProducts");
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

                    //Publish here to rabbitmq/ Azure Service Bus
                    await _publishEndpoint.Publish<CreateBidEvent>(result);

                    _logger.LogInformation($"Bid is created with Id {result.Id}");
                    return Ok();
                }
                else
                {
                    throw new NotFoundException("Product is not found");
                }
            }
            catch(BidAlreadyPlacedByUserException ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest();
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex.Message);
                var items = ex.Errors.SelectMany(d => d.Value).ToList();
                return BadRequest(string.Join(";", items));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500);
            }
        }


        [HttpPut("updateBidAmount/{id}")]
        public async Task<ActionResult<Unit>> UpdateBidAmount(string id, UpdateBidCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            try
            {
                _logger.LogInformation("Updating Bid");
                var result = await _mediator.Send(command);
                _logger.LogInformation($"Bid is updated");

                //Get updated Bid Info
                var bidResult = await _mediator.Send(new GetBidQuery(id));

                //Publish here to rabbitmq/ Azure Service Bus
                await _publishEndpoint.Publish<UpdateBidEvent>(bidResult);

                
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500);
            }
        }
    }
}
