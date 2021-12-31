using AutoMapper;
using Seller.Mediator.Library.Commands;
using Seller.Mediator.Library.Domain;
using Seller.Mediator.Library.ViewModels;

namespace Seller.Mediator.Library.Mapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDetails>().ReverseMap();
            CreateMap<Product,CreateProductCommand>().ReverseMap();
        }
    }
}
