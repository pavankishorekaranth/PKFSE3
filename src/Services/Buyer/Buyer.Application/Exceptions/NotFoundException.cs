using System;

namespace Buyer.Application.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string msg): base(msg)
        {

        }
    }
}
