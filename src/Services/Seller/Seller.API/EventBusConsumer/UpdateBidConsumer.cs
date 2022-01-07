using AutoMapper;
using EventBus.Message.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Seller.Application.Commands;
using System.Threading.Tasks;

namespace Seller.API.EventBusConsumer
{
    public class UpdateBidConsumer : IConsumer<UpdateBidEvent>
    {

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateBidConsumer> _logger;

        public UpdateBidConsumer(IMediator mediator, IMapper mapper, ILogger<CreateBidConsumer> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<UpdateBidEvent> context)
        {
            var command = _mapper.Map<UpdateBidCommand>(context.Message);
            var result = await _mediator.Send(command);

            _logger.LogInformation("UpdateBidEvent consumed successfully");
        }

    }
}
