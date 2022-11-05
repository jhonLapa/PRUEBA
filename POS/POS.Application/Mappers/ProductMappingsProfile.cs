using AutoMapper;
using POS.Application.Commons.Base;
using POS.Application.Dtos.Product.Request;
using POS.Application.Dtos.Product.Response;
using POS.Domain.Entities;
using POS.Infrastructure.Commons.Bases.Response;
using POS.Utilities.Static;

namespace POS.Application.Mappers
{
    public class ProductMappingsProfile : Profile
    {
        public ProductMappingsProfile()
        {
            CreateMap<Product, ProductResponseDto>()
              .ForMember(x => x.ProductId, x => x.MapFrom(y => y.Id))
              .ForMember(x => x.StateProduct, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Active) ? "Activo" : "Inactivo"))
            .ReverseMap();

            CreateMap<BaseEntityResponse<Product>, BaseEntityResponse<ProductResponseDto>>().ReverseMap();

            CreateMap<Product, ProductSelectResponseDto>()
             .ForMember(x => x.ProductId, x => x.MapFrom(y => y.Id))
             .ReverseMap();

            CreateMap<ProductRequestDto, Product>();
        }
    }
}
