using Buyer.Application.Contracts.Persistence;
using Buyer.Domain.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Buyer.Infrastructure.Persistence
{
    public class BuyerContext : IBuyerContext
    {
        public BuyerContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Bids = database.GetCollection<Bid>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
        }
        public IMongoCollection<Bid> Bids { get; }
    }
}
