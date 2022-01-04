using Buyer.API.Data;
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
        private readonly IBidRepository _bidRepository;
        private readonly IBuyerContext _buyerContext;

        public BidService(IBidRepository bidRepository, IBuyerContext buyerContext)
        {
            _bidRepository = bidRepository;
            _buyerContext = buyerContext;
        }

        public async Task<bool> IsBidForProductAlreadyExists(string productId, string email)
        {
            return await _bidRepository.IsBidForProductAlreadyExists(productId,email);
        }

        public async Task PlaceBid(Bid bid)
        {
            if (await IsBidForProductAlreadyExists(bid.ProductId, bid.Email))
                throw new BidAlreadyPlacedByUserException($"The bid already has been placed by {bid.Email} for productId {bid.ProductId}");
           
            await _bidRepository.PlaceBid(bid);
        }

        public async Task<bool> UpdateBid(string productId, string buyerEmail, decimal newAmount)
        {
            return await _bidRepository.UpdateBid(productId, buyerEmail, newAmount);
        }
    }
}
