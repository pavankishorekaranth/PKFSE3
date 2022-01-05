using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Seller.Application.Exceptions
{
    public class ProductNotExist : ApplicationException
    {
        public ProductNotExist(string message): base(message)
        {

        }
    }
}
