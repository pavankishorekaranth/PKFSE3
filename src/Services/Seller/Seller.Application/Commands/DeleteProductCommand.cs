using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Seller.Application.Commands
{
    public class DeleteProductCommand : IRequest
    {
        public string ProductId { get; set; }
    }
}
