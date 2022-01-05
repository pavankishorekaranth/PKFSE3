using AutoMapper;
using Buyer.Domain.Entities;
using Buyer.Application.Commands;
using Buyer.Application.Contracts.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Buyer.Application.Handlers
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
            var newBid= await _bidRepository.PlaceBid(bidEntity);

            _logger.LogInformation($"Bid {newBid.BidId} is successfully created.");

            return newBid.BidId;
        }
    }
}
