using MongoDB.Driver;
using Seller.Application.Contracts.Persistence;
using Seller.Domain.Entities;
using System.Threading.Tasks;

namespace Seller.Infrastructure.Repositaries
{
    public class BidRepository : IBidRepository
    {
        private readonly ISellerContext _context;

        public BidRepository(ISellerContext context)
        {
            _context = context;
        }

        public async Task<Bid> CreateBid(Bid bid)
        {
            await _context.Bids.InsertOneAsync(bid);
            return bid;
        }

        public async Task<bool> UpdateBid(Bid bidRecord)
        {
            //var bidRecord = await _context.Bids.Find(p => p.ProductId == productId && p.Email == buyerEmail).FirstOrDefaultAsync();
            //bidRecord.BidAmount = newAmount;

            var builders = Builders<Bid>.Filter.Eq(x => x.Id,bidRecord.Id);
            var updatedResult = await _context.Bids.ReplaceOneAsync(builders, bidRecord);

            //var updatedResult = await _context.Bids.ReplaceOneAsync(filter: x => x.ProductId == bidRecord.ProductId && x.Email == bidRecord.Email, replacement: bidRecord);

            return updatedResult.IsAcknowledged && updatedResult.ModifiedCount > 0;
        }
    }
}
