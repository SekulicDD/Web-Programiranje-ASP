using Application.DataTransfer.Cart;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Mappers
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<Cart, GetCartDto>()
                .ForMember(dto => dto.FirstName, opt => opt.MapFrom(x=>x.User.FirstName))
                .ForMember(dto => dto.LastName, opt => opt.MapFrom(x => x.User.LastName))
                .ForMember(dto => dto.Email, opt => opt.MapFrom(x => x.User.Email))
                .ForMember(dto => dto.ProductName, opt => opt.MapFrom(x => x.Product.Name));
            CreateMap<GetCartDto, Cart>();

            CreateMap<Cart, CreateCartDto>();
            CreateMap<CreateCartDto, Cart>();
        }
    }
}
