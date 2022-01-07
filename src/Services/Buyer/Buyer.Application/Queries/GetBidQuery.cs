using Buyer.Application.ViewModel;
using MediatR;

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
