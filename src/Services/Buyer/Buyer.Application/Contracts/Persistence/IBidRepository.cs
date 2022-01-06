using Buyer.Domain.Entities;
using System.Threading.Tasks;

namespace Buyer.Application.Contracts.Persistence
{
    public interface IBidRepository
    {
        Task<Bid> GetBidById(string id);
        Task<Bid> GetBidByProductIdAndEmail(string productId, string buyerEmail);
        Task<Bid> PlaceBid(Bid bid);
        Task<bool> UpdateBid(Bid bid);
        Task<bool> IsBidForProductAlreadyExists(string productId, string email);
    }
}
