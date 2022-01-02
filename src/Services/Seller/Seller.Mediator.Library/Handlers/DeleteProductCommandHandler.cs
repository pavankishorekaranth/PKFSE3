using MediatR;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using Seller.API.Features.Commands.DeleteProduct;
using Seller.Mediator.Library.DataAccess;
using Seller.Mediator.Library.Exceptions;
using Seller.Mediator.Library.Repositaries;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Seller.Mediator.Library.Handlers
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<DeleteProductCommandHandler> _logger;
        private readonly ISellerContext _context;

        public DeleteProductCommandHandler(IProductRepository productRepository, ILogger<DeleteProductCommandHandler> logger, ISellerContext context)
        {
            _productRepository = productRepository;
            _logger = logger;
            _context = context;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var productToDelete = await _productRepository.GetProductById(request.ProductId);
            if (productToDelete == null)
            {
                _logger.LogError($"Product with Id {request.ProductId} doesnot exist");
                throw new ProductNotExist($"Product with {request.ProductId} doesnot exists");
            }

            if(DateTime.Now > productToDelete.BidEndDate)
            {
                throw new BidEndDateException("Product cannot be deleted after Bid end date");
            }

            var count = await _context.Bids.Find(x=> x.ProductId== productToDelete.Id).CountDocumentsAsync();

            if (count > 0)
            {
                throw new DeleteProductForAtleastOneBidException($"There are currently {count} bids for this product. So this product cannot be deleted");
            }

            await _productRepository.DeleteProduct(request.ProductId);

            _logger.LogInformation($"Order {productToDelete.Id} is successfully deleted.");

            return Unit.Value;
        }
    }
}
