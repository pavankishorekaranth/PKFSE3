using Buyer.API.Entity;
using Buyer.API.Repositaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Buyer.API.Service
{
    public class BidService : IBidService
    {
        private readonly IBidRepository bidRepository;

        public BidService(IBidRepository bidRepository)
        {
            this.bidRepository = bidRepository;
        }

        public Task PlaceBid(Bid bid)
        {
            return bidRepository.PlaceBid(bid);
        }

        public Task<bool> UpdateBid(string productId, string buyerEmail, decimal newAmount)
        {
            return bidRepository.UpdateBid(productId, buyerEmail, newAmount);
        }
    }
}
