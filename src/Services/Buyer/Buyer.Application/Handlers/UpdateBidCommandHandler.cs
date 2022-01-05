using AutoMapper;
using Buyer.Domain.Entities;
using Buyer.Application.Commands;
using Buyer.Application.Contracts.Persistence;
using Buyer.Application.Exceptions;
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
    public class UpdateBidCommandHandler : IRequestHandler<UpdateBidCommand>
    {
        private readonly IBidRepository _bidRepository;
        private readonly ILogger<UpdateBidCommandHandler> _logger;
        private readonly IMapper _mapper;

        public UpdateBidCommandHandler(IBidRepository bidRepository, ILogger<UpdateBidCommandHandler> logger, IMapper mapper)
        {
            _bidRepository = bidRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateBidCommand request, CancellationToken cancellationToken)
        {
            var bidToUpdate = await _bidRepository.GetBidByIdAndEmail(request.Id,request.Email);
            if (bidToUpdate == null)
            {
                _logger.LogError($"Bid doesnot exist");
                throw new NotFoundException($"Bid with {request.Id} and {request.Email} doesnot exists");
            }

            _mapper.Map(request, bidToUpdate, typeof(UpdateBidCommand), typeof(Bid));

            await _bidRepository.UpdateBid(bidToUpdate);

            _logger.LogInformation($"Bid Amount {bidToUpdate.BidId} is successfully updated.");

            return Unit.Value;
        }
    }
}
