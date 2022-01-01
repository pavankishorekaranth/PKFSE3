using MediatR;
using Seller.Mediator.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seller.Mediator.Library.Queries
{
    public class GetAllProductsQuery : IRequest<List<ProductDetails>>
    {

    }
}
