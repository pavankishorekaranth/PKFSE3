using AutoMapper;
using EventBus.Message.Events;
using Seller.Application.Commands;

namespace Seller.API.Mapper
{
    public class SellerProfile : Profile
    {
        public SellerProfile()
        {
            CreateMap<CreateBidCommand, CreateBidEvent>().ReverseMap();
            CreateMap<UpdateBidCommand, UpdateBidEvent>().ReverseMap();
        }
    }
}
