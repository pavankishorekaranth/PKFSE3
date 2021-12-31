using Buyer.API.Entity;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Buyer.API.Data
{
    public interface IBuyerContext
    {
        IMongoCollection<Bid> Bids { get; }
    }
}
