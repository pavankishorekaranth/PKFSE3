using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Seller.API.Exceptions
{
    public class BidEndDateException: ApplicationException
    {
        public BidEndDateException(string msg): base(msg)
        {

        }
    }

}
