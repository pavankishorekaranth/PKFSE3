using MediatR;
using Microsoft.Extensions.Logging;
using Seller.API.Exceptions;
using Seller.API.Repositaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Seller.API.Features.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IProductRepository productRepository;
        private readonly ILogger<DeleteProductCommandHandler> logger;

        public DeleteProductCommandHandler(IProductRepository productRepository, ILogger<DeleteProductCommandHandler> logger)
        {
            this.productRepository = productRepository;
            this.logger = logger;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var productToDelete = await productRepository.GetProductById(request.ProductId);
            if (productToDelete == null)
            {
                throw new ProductNotExist($"Product with {request.ProductId} doesnot exists");
            }

            await productRepository.DeleteProduct(request.ProductId);

            logger.LogInformation($"Order {productToDelete.ProductId} is successfully deleted.");

            return Unit.Value;
        }
    }
}
