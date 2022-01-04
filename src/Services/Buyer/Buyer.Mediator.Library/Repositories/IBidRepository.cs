using Buyer.Mediator.Library.Domain;
using System.Threading.Tasks;

namespace Buyer.Mediator.Library.Repositories
{
    public interface IBidRepository
    {
        Task<Bid> GetBidByIdAndEmail(string productId, string buyerEmail);
        Task<Bid> PlaceBid(Bid bid);
        Task<bool> UpdateBid(Bid bid);
        Task<bool> IsBidForProductAlreadyExists(string productId, string email);
    }
}
