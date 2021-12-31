using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Seller.Mediator.Library.DataAccess;
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

        public GetAllBidsForProductQueryHandler(ISellerContext context, IMapper mapper, ILogger<GetAllProductsQueryHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public Task<ProductBidDetails> Handle(GetAllBidsForProductQuery request, CancellationToken cancellationToken)
        {
            string productId = request.ProductId;
            throw new NotImplementedException();
        }
    }
}
