using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Seller.Mediator.Library.Domain;

namespace Seller.Mediator.Library.DataAccess
{
    public class SellerContext : ISellerContext
    {
        public SellerContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            Bids = database.GetCollection<Bid>(configuration.GetValue<string>("Bid"));
        }

        public IMongoCollection<Product> Products { get; }
        public IMongoCollection<Bid> Bids { get; }
    }
}
