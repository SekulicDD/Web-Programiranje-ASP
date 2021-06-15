using Application.DataTransfer;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Mappers
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, CreateProductDto>()
                .ForMember(dto => dto.CategoryIds, opt => opt.MapFrom(x => x.ProductCategories.Select(c => c.CategoryId)));
            CreateMap<CreateProductDto, Product>()
                .ForMember(product => product.ProductCategories,
                opt => opt.MapFrom((x,y) => x.CategoryIds.Select(id => new ProductCategory { CategoryId = id, ProductId=y.Id })));

            CreateMap<Product, GetProductDto>()
                .ForMember(dto => dto.Categories, opt => opt.MapFrom(x => x.ProductCategories.Select(c => c.Category.Name)));
            CreateMap<GetProductDto, Product>();
        }
    }
}
