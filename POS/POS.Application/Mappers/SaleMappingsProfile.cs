using AutoMapper;
using POS.Application.Dtos.Sale.Request;
using POS.Application.Dtos.Sale.Response;
using POS.Domain.Entities;
using POS.Infrastructure.Commons.Bases.Response;
using POS.Infrastructure.Commons.Sale.Request;
using POS.Infrastructure.Commons.Sale.Response;
using POS.Utilities.Static;

namespace POS.Application.Mappers
{
    public class SaleMappingsProfile : Profile
    {
        public SaleMappingsProfile()
        {
            CreateMap<Sale, SaleResponseDto>()
              .ForMember(x => x.SaleId, x => x.MapFrom(y => y.Id))
              .ForMember(x => x.StateSale, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Active) ? "Activo" : "Inactivo"))
            .ReverseMap();
            CreateMap<sp_ventRequestDto, sp_CrearVentaEntityRequest>();
            CreateMap<sp_EliminarSaleRequest, EliminarSaleEntityRequest>();
            CreateMap<sp_EliminarSaleRequest, sp_EliminarSaleDetalleEntityRequest>();
            CreateMap<sp_ventRequestDto, sp_EditarVentaEntityRequest>();

            


            CreateMap<sp_ListaComprasUsersEntityResponse, sp_ListaComprasUsersResponse>()
               .ReverseMap();

            CreateMap<SaleDetail, SaleDetailByIdResponseDto>()
            .ForMember(x => x.SaleDetailId, x => x.MapFrom(y => y.SaleDetailId))
            .ForMember(x => x.Amount, x => x.MapFrom(y => y.Amount))
            .ForMember(x => x.SaleId, x => x.MapFrom(y => y.SaleId))
             .ForMember(x => x.PriceTotalProduct, x => x.MapFrom(y => y.PriceTotalProduct))
            .ForMember(x => x.Code, x => x.MapFrom(y => y.SaleId))
            .ForMember(x => x.ProductId, x => x.MapFrom(y => y.ProductId))
            .ForMember(x => x.Code, x => x.MapFrom(y => y.Product.Code))
            .ForMember(x => x.Name, x => x.MapFrom(y => y.Product.Name))
            .ForMember(x => x.Stock, x => x.MapFrom(y => y.Product.Stock))
            .ForMember(x => x.Image, x => x.MapFrom(y => y.Product.Image))
            .ForMember(x => x.SellPrice, x => x.MapFrom(y => y.Product.SellPrice))
            .ForMember(x => x.CategoryId, x => x.MapFrom(y => y.Product.CategoryId))
            .ForMember(x => x.Category, x => x.MapFrom(y => y.Product.Category.Name))

            .ReverseMap();
        }
    }
}
