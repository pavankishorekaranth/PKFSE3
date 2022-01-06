using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buyer.Application.Exceptions
{
    public class BidEndDateExpiredException : ApplicationException
    {
        public BidEndDateExpiredException(string msg): base(msg)
        {

        }
    }
}
