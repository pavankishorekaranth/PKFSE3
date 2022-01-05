using Buyer.Application.Commands;
using Buyer.Application.Exceptions;
using EventBus.Message.Events;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
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

        public BuyerController(ILogger<BuyerController> logger, IMediator mediator, IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _mediator = mediator;
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost("place-bid")]
        public async Task<ActionResult> PlaceBid([FromBody] CreateBidCommand bid)
        {
            try
            {
                _logger.LogInformation("Creating Bid");
                var result = await _mediator.Send(bid);

                //Publish here to rabbitmq/ Azure Service Bus
                await _publishEndpoint.Publish<CreateBidEvent>(result);

                _logger.LogInformation($"Bid is created with Id {result.Id}");
                return Ok();
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
                _logger.LogInformation("Creating Bid");
                var result = await _mediator.Send(command);

                _logger.LogInformation($"Bid is updated");
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
