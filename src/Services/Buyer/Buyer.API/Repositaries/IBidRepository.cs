using Buyer.API.Entity;
using System.Threading.Tasks;

namespace Buyer.API.Repositaries
{
    public interface IBidRepository
    {
        Task PlaceBid(Bid bid);
        Task<bool> UpdateBid(string productId, string buyerEmail, decimal newAmount);
        Task<bool> IsBidByProductIdAndEmailExists(string productId, string email);
    }
}
