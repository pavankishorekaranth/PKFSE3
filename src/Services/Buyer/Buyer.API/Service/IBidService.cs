using Buyer.API.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Buyer.API.Service
{
    public interface IBidService
    {
        Task PlaceBid(Bid bid);
        Task<bool> UpdateBid(string productId, string buyerEmail, decimal newAmount);
        Task<bool> IsBidForProductAlreadyExists(string productId, string email);
    }
}
