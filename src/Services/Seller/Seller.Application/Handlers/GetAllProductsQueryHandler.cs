using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Seller.Application.Contracts.Persistence;
using Seller.Application.Queries;
using Seller.Application.ViewModels;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Seller.Application.Handlers
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<ProductDetails>>
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllProductsQueryHandler> _logger;

        public GetAllProductsQueryHandler(IProductRepository productRepository, IMapper mapper, ILogger<GetAllProductsQueryHandler> logger)
        {
            this.productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<ProductDetails>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling get all Products query method");
            var productList = await productRepository.GetProducts();
            if (productList == null)
            {
                _logger.LogWarning("No records available in GetAllProductsQueryHandler method");
                return null;
            }

            _logger.LogInformation("Completed handling get all Products query method");
            return _mapper.Map<List<ProductDetails>>(productList);
        }
    }
}
