using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Seller.API.Features.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest
    {
        public string ProductId { get; set; }
    }
}
