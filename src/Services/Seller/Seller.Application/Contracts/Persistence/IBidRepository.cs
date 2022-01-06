using Seller.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seller.Application.Contracts.Persistence
{
    public interface IBidRepository
    {
        Task<Bid> CreateBid(Bid bid);
        Task<bool> UpdateBid(Bid bid);
    }
}
