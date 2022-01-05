using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Seller.Domain.Entities;
using Seller.Application.Contracts.Persistence;
using Seller.Application.Exceptions;
using Seller.Application.Queries;
using Seller.Application.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Seller.Application.Handlers
{
    public class GetAllBidsForProductQueryHandler : IRequestHandler<GetAllBidsForProductQuery, ProductBidDetails>
    {
        private readonly ISellerContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllProductsQueryHandler> _logger;
        private readonly IProductRepository _productRepository;

        public GetAllBidsForProductQueryHandler(ISellerContext context, IMapper mapper, ILogger<GetAllProductsQueryHandler> logger, IProductRepository productRepository)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _productRepository = productRepository;
        }

        public async Task<ProductBidDetails> Handle(GetAllBidsForProductQuery request, CancellationToken cancellationToken)
        {
            string productId = request.ProductId;

            var product = await _productRepository.GetProductById(request.ProductId);
            if (product == null)
            {
                _logger.LogError($"Product with Id {request.ProductId} doesnot exist");
                throw new ProductNotExist($"Product with {request.ProductId} doesnot exists");
            }

            List<Bid> bids = await _context.Products.AsQueryable()
                                    .Where(j => j.Id == request.ProductId)
                                    .Join(
                                       _context.Bids, //foreign collection
                                        j => j.Id, //local ID
                                        b => b.ProductId, //foreign ID
                                        (j, b) => b) //result selector expression
                                    .ToListAsync();

            var productDetails = _mapper.Map<ProductBidDetails>(product);
            productDetails.Bids = _mapper.Map<List<BidDetails>>(bids);
           

            return productDetails;
        }
    }
}
