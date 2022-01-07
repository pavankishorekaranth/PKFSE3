using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Seller.Application.Commands;
using Seller.Application.Contracts.Persistence;
using Seller.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Seller.Application.Handlers
{
    public class UpdateBidCommandHandler : IRequestHandler<UpdateBidCommand>
    {
        private readonly IMapper _mapper;
        private readonly IBidRepository _bidRepository;
        private readonly ILogger<UpdateBidCommandHandler> _logger;

        public UpdateBidCommandHandler(IMapper mapper, IBidRepository bidRepository, ILogger<UpdateBidCommandHandler> logger)
        {
            _mapper = mapper;
            _bidRepository = bidRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateBidCommand request, CancellationToken cancellationToken)
        {
            var bidEntity = _mapper.Map<Bid>(request);
            var updateBid = await _bidRepository.UpdateBid(bidEntity);

            _logger.LogInformation($"Bid Information is successfully updated.");

            return Unit.Value;
        }
    }
}
