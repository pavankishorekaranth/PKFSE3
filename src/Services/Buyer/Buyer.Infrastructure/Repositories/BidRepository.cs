using Buyer.Domain.Entities;
using Buyer.Application.Contracts.Persistence;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buyer.Infrastructure.Repositories
{
    public class BidRepository : IBidRepository
    {
        private readonly IBuyerContext _context;

        public BidRepository(IBuyerContext context)
        {
            _context = context;
        }

        public async Task<Bid> GetBidById(string id)
        {
            return await _context.Bids.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Bid> PlaceBid(Bid bid)
        {
            await _context.Bids.InsertOneAsync(bid);
            return bid;
        }

        public async Task<Bid> GetBidByProductIdAndEmail(string productId, string email)
        {
            return await _context.Bids.Find(p => p.ProductId == productId && p.Email == email).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateBid(Bid bidRecord)
        {
            //var bidRecord = await _context.Bids.Find(p => p.ProductId == productId && p.Email == buyerEmail).FirstOrDefaultAsync();
            //bidRecord.BidAmount = newAmount;

            var updatedResult = await _context.Bids.ReplaceOneAsync(filter: p => p.ProductId == bidRecord.ProductId && p.Email == bidRecord.Email, replacement: bidRecord);
            return updatedResult.IsAcknowledged && updatedResult.ModifiedCount > 0;
        }

        public async Task<bool> IsBidForProductAlreadyExists(string productId, string email)
        {
            return await _context.Bids.Find(p => p.ProductId == productId && p.Email == email).CountDocumentsAsync() >= 1;
        }
    }
}
