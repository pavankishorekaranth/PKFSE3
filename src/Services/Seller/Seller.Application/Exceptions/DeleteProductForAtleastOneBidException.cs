using System;

namespace Seller.Application.Exceptions
{
    public class DeleteProductForAtleastOneBidException : ApplicationException
    {
        public DeleteProductForAtleastOneBidException(string message): base(message)
        {

        }
    }
}
