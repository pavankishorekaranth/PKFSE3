using Buyer.Domain.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Buyer.Application.Contracts.Persistence
{
    public interface IBuyerContext
    {
        IMongoCollection<Bid> Bids { get; }
    }
}
