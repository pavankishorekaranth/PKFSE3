using Seller.Domain.Entities;
using Seller.Application.Contracts.Persistence;
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
    }
}
