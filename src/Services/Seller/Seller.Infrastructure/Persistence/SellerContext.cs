using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Seller.Application.Contracts.Persistence;
using Seller.Domain.Entities;

namespace Seller.Infrastructure.Persistence
{
    public class SellerContext : ISellerContext
    {
        public SellerContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:ProductCollectionName"));
            Bids = database.GetCollection<Bid>(configuration.GetValue<string>("DatabaseSettings:BidCollectionName"));
        }

        public IMongoCollection<Product> Products { get; }
        public IMongoCollection<Bid> Bids { get; }
    }
}
