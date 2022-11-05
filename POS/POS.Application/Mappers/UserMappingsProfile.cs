using AutoMapper;
using POS.Application.Dtos.User.Request;
using POS.Application.Dtos.User.Response;
using POS.Domain.Entities;
using POS.Infrastructure.Commons.Bases.Response;
using POS.Utilities.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Mappers
{
    public class UserMappingsProfile : Profile
    {
        public UserMappingsProfile()
        {
            CreateMap<User, UserResponseDto>()
              .ForMember(x => x.UserId, x => x.MapFrom(y => y.Id))
              .ForMember(x => x.StateUser, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Active) ? "Activo" : "Inactivo"))
            .ReverseMap();

            CreateMap<BaseEntityResponse<User>, BaseEntityResponse<UserResponseDto>>().ReverseMap();

            CreateMap<UserRequestDto, User>();

            CreateMap<TokenRequestDto, User>();

        }
    }
}
