using System;

namespace Buyer.Application.Exceptions
{
    public class BidEndDateExpiredException : ApplicationException
    {
        public BidEndDateExpiredException(string msg): base(msg)
        {

        }
    }
}
