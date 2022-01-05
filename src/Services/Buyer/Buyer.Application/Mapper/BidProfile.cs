using AutoMapper;
using Buyer.Application.ViewModel;
using Buyer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buyer.Application.Mapper
{
    public class BidProfile : Profile
    {
        public BidProfile()
        {
            CreateMap<Bid, BidInfo>().ReverseMap();
        }
    }
}
