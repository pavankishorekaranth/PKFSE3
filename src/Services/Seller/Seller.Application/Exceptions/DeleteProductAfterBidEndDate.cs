using System;

namespace Seller.Application.Exceptions
{
    public class DeleteProductAfterBidEndDate : ApplicationException
    {
        public DeleteProductAfterBidEndDate(string message): base(message)
        {

        }
    }
}
