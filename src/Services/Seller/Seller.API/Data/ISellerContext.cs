using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Seller.API.Entity;

namespace Seller.API.Data
{
    public interface ISellerContext
    {
        IMongoCollection<Product> Products { get; }
    }
}
