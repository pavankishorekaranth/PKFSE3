using MongoDB.Driver;
using Seller.Mediator.Library.Domain;

namespace Seller.Mediator.Library.DataAccess
{
    public interface ISellerContext
    {
        IMongoCollection<Product> Products { get; }
        IMongoCollection<Bid> Bids { get;}
    }
}
