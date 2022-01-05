﻿using AutoMapper;
using Buyer.Application.ViewModel;
using EventBus.Message.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
