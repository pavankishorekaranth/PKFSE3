using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Seller.Domain.Entities;
using Seller.Application.Commands;
using Seller.Application.Contracts.Persistence;
using Seller.Application.Exceptions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Seller.Application.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, string>
    {
        private readonly ILogger<CreateProductCommandHandler> _logger;
        private readonly IProductRepository productRepository;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(ILogger<CreateProductCommandHandler> logger, IProductRepository productRepository, IMapper mapper)
        {
            _logger = logger;
            this.productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<string> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            decimal val;
            if(!(request.StartingPrice > 0 && Decimal.TryParse(request.StartingPrice.ToString(), out val)))
            {
                throw new MustBeNumberException("Must be a postive number");
            }

            if(DateTime.UtcNow > request.BidEndDate)
            {
                throw new BidEndDateException("Bid End Date must be future date");
            }

            var productEntity = _mapper.Map<Product>(request);
            var newOrder = await productRepository.CreateProduct(productEntity);

            _logger.LogInformation($"Order {newOrder.Id} is successfully created.");

            return newOrder.Id;
        }
    }
}
