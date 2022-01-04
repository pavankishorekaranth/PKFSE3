using Buyer.Mediator.Library.Domain;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Buyer.Mediator.Library.DataAccess
{
    public interface IBuyerContext
    {
        IMongoCollection<Bid> Bids { get; }
    }
}
