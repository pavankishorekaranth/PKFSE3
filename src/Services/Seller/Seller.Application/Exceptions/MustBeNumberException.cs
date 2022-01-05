using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seller.Application.Exceptions
{
    public class MustBeNumberException : ApplicationException
    {
        public MustBeNumberException(string msg): base(msg)
        {

        }
    }
}
