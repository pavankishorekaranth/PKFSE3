using System;

namespace Seller.Application.Exceptions
{
    public class MustBeNumberException : ApplicationException
    {
        public MustBeNumberException(string msg): base(msg)
        {

        }
    }
}
