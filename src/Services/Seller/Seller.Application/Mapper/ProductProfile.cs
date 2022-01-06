using AutoMapper;
using Seller.Domain.Entities;
using Seller.Application.Commands;
using Seller.Application.ViewModels;

namespace Seller.Application.Mapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDetails>().ReverseMap();
            CreateMap<Product,CreateProductCommand>().ReverseMap();
            CreateMap<Bid, BidDetails>().ReverseMap();
            CreateMap<Product, ProductBidDetails>().ReverseMap();
            CreateMap<Bid, CreateBidCommand>().ReverseMap();
            CreateMap<Bid, UpdateBidCommand>().ReverseMap();
        }
    }
}
