using AutoMapper;
using Buyer.Application.Commands;
using Buyer.Application.Contracts.Persistence;
using Buyer.Application.Exceptions;
using Buyer.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
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
            var bidToUpdate = await _bidRepository.GetBidByProductIdAndEmail(request.ProductId,request.Email);
            if (bidToUpdate == null)
            {
                _logger.LogError($"Bid doesnot exist");
                throw new NotFoundException($"Bid with {request.Id} and {request.Email} doesnot exists");
            }

            _mapper.Map(request, bidToUpdate, typeof(UpdateBidCommand), typeof(Bid));

            await _bidRepository.UpdateBid(bidToUpdate);

            _logger.LogInformation($"Bid Amount {bidToUpdate.Id} is successfully updated.");

            return Unit.Value;
        }
    }
}
