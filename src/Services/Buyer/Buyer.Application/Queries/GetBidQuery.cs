using Buyer.Application.ViewModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buyer.Application.Queries
{
    public class GetBidQuery : IRequest<BidInfo>
    {
        public string Id { get; set; }

        public GetBidQuery(string BidId)
        {
            Id = BidId;
        }
    }
}
