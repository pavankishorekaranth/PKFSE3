using Seller.Mediator.Library.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seller.Mediator.Library.Repositaries
{
    public interface IBidRepository
    {
        Task<Bid> CreateBid(Bid bid);
    }
}
