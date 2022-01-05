using MediatR;
using Seller.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
