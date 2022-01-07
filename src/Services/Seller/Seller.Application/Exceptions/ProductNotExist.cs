using System;

namespace Seller.Application.Exceptions
{
    public class ProductNotExist : ApplicationException
    {
        public ProductNotExist(string message): base(message)
        {

        }
    }
}
