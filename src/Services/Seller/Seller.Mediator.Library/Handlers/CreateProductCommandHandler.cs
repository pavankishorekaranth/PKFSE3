using MediatR;
using Microsoft.Extensions.Logging;
using Seller.Mediator.Library.Commands;
using Seller.Mediator.Library.Repositaries;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Seller.Mediator.Library.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly ILogger<CreateProductCommandHandler> _logger;
        private readonly IProductRepository productRepository;

        public CreateProductCommandHandler(ILogger<CreateProductCommandHandler> logger, IProductRepository productRepository)
        {
            _logger = logger;
            this.productRepository = productRepository;
        }

        public Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
