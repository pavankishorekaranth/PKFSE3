using AutoMapper;
using EventBus.Message.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Seller.Application.Commands;
using System.Threading.Tasks;

namespace Seller.API.EventBusConsumer
{
    public class CreateBidConsumer : IConsumer<CreateBidEvent>
    {

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateBidConsumer> _logger;

        public CreateBidConsumer(IMediator mediator, IMapper mapper, ILogger<CreateBidConsumer> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<CreateBidEvent> context)
        {
            var command = _mapper.Map<CreateBidCommand>(context.Message);
            var result = await _mediator.Send(command);

            _logger.LogInformation("CreateBidEvent consumed successfully. Created Bid Id : {newBidId}", result);
        }
    }
}
