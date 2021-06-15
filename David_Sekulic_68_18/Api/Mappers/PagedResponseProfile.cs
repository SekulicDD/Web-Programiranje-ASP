using Application.DataTransfer;
using Application.DataTransfer.Cart;
using Application.Searches;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Mappers
{
    public class PagedResponseProfile : Profile
    {
        public PagedResponseProfile()
        {
            CreateMap<CartSearch, PagedResponse<GetCartDto>>()
                .ForMember(dto => dto.CurrentPage, opt => opt.MapFrom(x => x.Page))
                .ForMember(dto => dto.ItemsPerPage, opt => opt.MapFrom(x => x.PerPage))
                .ForMember(dto => dto.TotalCount, opt => opt.MapFrom(x => x.Page));

            CreateMap<OrderSearch, PagedResponse<GetOrderDto>>()
                .ForMember(dto => dto.CurrentPage, opt => opt.MapFrom(x => x.Page))
                .ForMember(dto => dto.ItemsPerPage, opt => opt.MapFrom(x => x.PerPage))
                .ForMember(dto => dto.TotalCount, opt => opt.MapFrom(x => x.Page));

            CreateMap<ProductSearch, PagedResponse<GetProductDto>>()
                .ForMember(dto => dto.CurrentPage, opt => opt.MapFrom(x => x.Page))
                .ForMember(dto => dto.ItemsPerPage, opt => opt.MapFrom(x => x.PerPage))
                .ForMember(dto => dto.TotalCount, opt => opt.MapFrom(x => x.Page));

            CreateMap<CategorySearch, PagedResponse<CategoryDto>>()
                .ForMember(dto => dto.CurrentPage, opt => opt.MapFrom(x => x.Page))
                .ForMember(dto => dto.ItemsPerPage, opt => opt.MapFrom(x => x.PerPage))
                .ForMember(dto => dto.TotalCount, opt => opt.MapFrom(x => x.Page));

        }
    }
}
