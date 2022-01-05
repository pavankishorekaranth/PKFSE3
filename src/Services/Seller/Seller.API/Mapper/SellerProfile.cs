using AutoMapper;
using EventBus.Message.Events;
using Seller.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Seller.API.Mapper
{
    public class SellerProfile : Profile
    {
        public SellerProfile()
        {
            CreateMap<CreateBidCommand, CreateBidEvent>().ReverseMap();
        }
    }
}
