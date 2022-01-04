using Buyer.API.Data;
using Buyer.API.Entity;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Buyer.API.Repositaries
{
    public class BidRepository : IBidRepository
    {
        private readonly IBuyerContext _context;

        public BidRepository(IBuyerContext context)
        {
            _context = context;
        }

        public async Task PlaceBid(Bid bid)
        {
             await _context.Bids.InsertOneAsync(bid);
        }

        public async Task<bool> UpdateBid(string productId, string buyerEmail, decimal newAmount)
        {
            var bidRecord = await _context.Bids.Find(p => p.ProductId == productId && p.Email==buyerEmail).FirstOrDefaultAsync();
            bidRecord.BidAmount = newAmount;

            var updatedResult = await _context.Bids.ReplaceOneAsync(filter: p=> p.ProductId== productId && p.Email==buyerEmail, replacement: bidRecord);

            return updatedResult.IsAcknowledged && updatedResult.ModifiedCount > 0;
        }

        public async Task<bool> IsBidForProductAlreadyExists(string productId, string email)
        {
            return await _context.Bids.Find(p => p.ProductId == productId && p.Email == email).CountDocumentsAsync() >=1;
        }
    }
}
