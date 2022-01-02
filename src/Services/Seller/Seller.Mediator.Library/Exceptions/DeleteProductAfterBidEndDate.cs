using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Seller.Mediator.Library.Exceptions
{
    public class DeleteProductAfterBidEndDate : ApplicationException
    {
        public DeleteProductAfterBidEndDate(string message): base(message)
        {

        }
    }
}
