using AutoMapper;
using Buyer.Application.Commands;
using Buyer.Application.ViewModel;
using Buyer.Domain.Entities;

namespace Buyer.Application.Mapper
{
    public class BidProfile : Profile
    {
        public BidProfile()
        {
            CreateMap<Bid, BidInfo>().ReverseMap();
            CreateMap<Bid, CreateBidCommand>().ReverseMap();
            CreateMap<Bid, UpdateBidCommand>().ReverseMap();
        }
    }
}
