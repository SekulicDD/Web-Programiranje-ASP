using Application.DataTransfer;
using Application.DataTransfer.Order;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Mappers
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, CreateOrderDto>();
            CreateMap<CreateOrderDto, Order>();

            CreateMap<Order, GetOrderDto>()
                .ForMember(dto => dto.FirstName, opt => opt.MapFrom(x => x.User.FirstName))
                .ForMember(dto => dto.LastName, opt => opt.MapFrom(x => x.User.LastName))
                .ForMember(dto => dto.Email, opt => opt.MapFrom(x => x.User.Email))
                .ForMember(dto => dto.ProductNames, opt => opt.MapFrom(o => o.OrderDetails.Where(od => od.OrderId == o.Id).Select(p => p.Product.Name)));

            CreateMap<GetOrderDto, Order>();
        }
    }
}
