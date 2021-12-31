using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Seller.Mediator.Library.Exceptions
{
    public class BidEndDateException: ApplicationException
    {
        public BidEndDateException(string msg): base(msg)
        {

        }
    }

}
