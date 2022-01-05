using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Buyer.Application.Exceptions
{
    public class BidAlreadyPlacedByUserException : ApplicationException
    {
        public BidAlreadyPlacedByUserException(string message): base(message)
        {

        }
    }
}
