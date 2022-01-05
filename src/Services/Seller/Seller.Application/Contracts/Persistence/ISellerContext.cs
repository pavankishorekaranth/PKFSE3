using MongoDB.Driver;
using Seller.Domain.Entities;

namespace Seller.Application.Contracts.Persistence
{
    public interface ISellerContext
    {
        IMongoCollection<Product> Products { get; }
        IMongoCollection<Bid> Bids { get; }
    }
}
