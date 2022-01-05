using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Seller.Application.Exceptions
{
    public class DeleteProductForAtleastOneBidException : ApplicationException
    {
        public DeleteProductForAtleastOneBidException(string message): base(message)
        {

        }
    }
}
