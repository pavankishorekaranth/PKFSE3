﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
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

            var bidsList = await _context.Bids.AsQueryable()
                                    .Where(j => j.ProductId == request.ProductId).ToListAsync(cancellationToken: cancellationToken);

            var productDetails = _mapper.Map<ProductBidDetails>(product);
            productDetails.Bids = _mapper.Map<List<BidDetails>>(bidsList);
           

            return productDetails;
        }
    }
}
