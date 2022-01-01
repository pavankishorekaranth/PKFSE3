using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Seller.Mediator.Library.DataAccess;
using Seller.Mediator.Library.Exceptions;
using Seller.Mediator.Library.Queries;
using Seller.Mediator.Library.Repositaries;
using Seller.Mediator.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Seller.Mediator.Library.Handlers
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

            var result = (from a in _context.Products.AsQueryable()
                         join b in _context.Bids.AsQueryable() on a.ProductId equals b.ProductId into bids
                         where a.ProductId == productId
                         select new ProductBidDetails
                         {
                             ProductId = a.ProductId,
                             ProductName = a.ProductName,
                             ShortDescription = a.ShortDescription,
                             DetailedDescription = a.DetailedDescription,
                             StartingPrice = a.StartingPrice,
                             BidEndDate = a.BidEndDate,
                             FirstName = a.FirstName,
                             LastName = a.LastName,
                             Address = a.Address,
                             City = a.City,
                             State = a.State,
                             Pin = a.Pin,
                             Phone = a.Phone,
                             Email = a.Email,
                             Bids = (bids!=null)? _mapper.Map<List<BidDetails>>(bids.OrderByDescending(x=>x.BidAmount).ToList()) : null
                         }).FirstOrDefault();

            return result;
        }
    }
}
