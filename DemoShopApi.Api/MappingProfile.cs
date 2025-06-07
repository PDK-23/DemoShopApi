using AutoMapper;
using DemoShopApi.Application.DTOs;

namespace DemoShopApi.Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<CreateProductDto, Product>();
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name));

        }
    }
}

