using AutoMapper;
using Buyer.Application.ViewModel;
using EventBus.Message.Events;

namespace Buyer.API.Mappers
{
    public class BuyerProfile : Profile
    {
        public BuyerProfile()
        {
            CreateMap<CreateBidEvent, BidInfo>().ReverseMap();
        }
    }
}
