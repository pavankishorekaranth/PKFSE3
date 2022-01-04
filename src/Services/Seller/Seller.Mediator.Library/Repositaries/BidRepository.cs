using Seller.Mediator.Library.DataAccess;
using Seller.Mediator.Library.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seller.Mediator.Library.Repositaries
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
