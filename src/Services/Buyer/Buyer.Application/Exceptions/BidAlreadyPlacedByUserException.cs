using System;

namespace Buyer.Application.Exceptions
{
    public class BidAlreadyPlacedByUserException : ApplicationException
    {
        public BidAlreadyPlacedByUserException(string message): base(message)
        {

        }
    }
}
