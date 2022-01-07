using System;

namespace Seller.Application.Exceptions
{
    public class BidEndDateException: ApplicationException
    {
        public BidEndDateException(string msg): base(msg)
        {

        }
    }

}
