using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Seller.Mediator.Library.Commands;
using Seller.Mediator.Library.Domain;
using Seller.Mediator.Library.Exceptions;
using Seller.Mediator.Library.Repositaries;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Seller.Mediator.Library.Handlers
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

            if(DateTime.Now > request.BidEndDate)
            {
                throw new BidEndDateException("Bid End Date must be future date");
            }

            var productEntity = _mapper.Map<Product>(request);
            var newOrder = await productRepository.CreateProduct(productEntity);

            _logger.LogInformation($"Order {newOrder.ProductId} is successfully created.");

            return newOrder.ProductId;
        }
    }
}
