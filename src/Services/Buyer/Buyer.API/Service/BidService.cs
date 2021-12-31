using Buyer.API.Entity;
using Buyer.API.Exceptions;
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

        public async Task<bool> IsBidByProductIdAndEmailExists(string productId, string email)
        {
            return await bidRepository.IsBidByProductIdAndEmailExists(productId,email);
        }

        public async Task PlaceBid(Bid bid)
        {
            if (await IsBidByProductIdAndEmailExists(bid.ProductId, bid.Email))
                throw new BidAlreadyPlacedByUserException($"The bid already has been placed by {bid.Email} for productId {bid.ProductId}");
           
            await bidRepository.PlaceBid(bid);
        }

        public async Task<bool> UpdateBid(string productId, string buyerEmail, decimal newAmount)
        {
            return await bidRepository.UpdateBid(productId, buyerEmail, newAmount);
        }
    }
}
