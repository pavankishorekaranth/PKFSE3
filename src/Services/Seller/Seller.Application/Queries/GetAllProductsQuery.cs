using MediatR;
using Seller.Application.ViewModels;
using System.Collections.Generic;

namespace Seller.Application.Queries
{
    public class GetAllProductsQuery : IRequest<List<ProductDetails>>
    {

    }
}
