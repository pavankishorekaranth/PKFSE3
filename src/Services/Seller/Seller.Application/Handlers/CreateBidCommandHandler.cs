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
    public class CreateBidCommandHandler : IRequestHandler<CreateBidCommand, string>
    {
        private readonly IMapper _mapper;
        private readonly IBidRepository _bidRepository;
        private readonly ILogger<CreateBidCommandHandler> _logger;

        public CreateBidCommandHandler(IMapper mapper, IBidRepository bidRepository, ILogger<CreateBidCommandHandler> logger)
        {
            _mapper = mapper;
            _bidRepository = bidRepository;
            _logger = logger;
        }

        public async Task<string> Handle(CreateBidCommand request, CancellationToken cancellationToken)
        {
            var bidEntity = _mapper.Map<Bid>(request);
            var newBid= await _bidRepository.CreateBid(bidEntity);

            _logger.LogInformation($"Bid {newBid.Id} is successfully created.");

            return newBid.Id;
        }
    }
}
