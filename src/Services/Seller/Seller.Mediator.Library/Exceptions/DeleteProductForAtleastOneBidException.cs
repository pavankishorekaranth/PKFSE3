using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Seller.Mediator.Library.Exceptions
{
    public class DeleteProductForAtleastOneBidException : ApplicationException
    {
        public DeleteProductForAtleastOneBidException(string message): base(message)
        {

        }
    }
}
