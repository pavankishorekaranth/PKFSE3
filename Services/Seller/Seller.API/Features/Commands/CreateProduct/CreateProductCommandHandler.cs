using MediatR;
using Microsoft.Extensions.Logging;
using Seller.API.Repositaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Seller.API.Features.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly ILogger<CreateProductCommandHandler> _logger;
        private readonly IProductRepository productRepository;

        public Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
