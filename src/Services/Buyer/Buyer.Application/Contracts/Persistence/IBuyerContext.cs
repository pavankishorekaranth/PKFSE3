using Buyer.Domain.Entities;
using MongoDB.Driver;

namespace Buyer.Application.Contracts.Persistence
{
    public interface IBuyerContext
    {
        IMongoCollection<Bid> Bids { get; }
    }
}
