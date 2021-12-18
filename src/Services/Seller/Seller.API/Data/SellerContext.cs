using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Seller.API.Entity;

namespace Seller.API.Data
{
    public class SellerContext : ISellerContext
    {
        public SellerContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
        }

        public IMongoCollection<Product> Products { get; }
    }
}
