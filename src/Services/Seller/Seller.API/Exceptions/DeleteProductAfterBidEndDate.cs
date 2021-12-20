﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Seller.API.Exceptions
{
    public class DeleteProductAfterBidEndDate : ApplicationException
    {
        public DeleteProductAfterBidEndDate(string message): base(message)
        {

        }
    }
}
