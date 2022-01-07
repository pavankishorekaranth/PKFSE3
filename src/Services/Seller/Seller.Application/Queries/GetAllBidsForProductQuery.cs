using MediatR;
using Seller.Application.ViewModels;

namespace Seller.Application.Queries
{
    public class GetAllBidsForProductQuery : IRequest<ProductBidDetails>
    {
        public string ProductId { get; set; }

        public GetAllBidsForProductQuery(string productId)
        {
            ProductId = productId;
        }
    }
}
