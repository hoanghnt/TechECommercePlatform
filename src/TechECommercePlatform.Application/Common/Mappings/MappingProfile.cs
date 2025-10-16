using AutoMapper;
using TechECommercePlatform.Application.Features.Products.DTOs;
using TechECommercePlatform.Domain.Entities;

namespace TechECommercePlatform.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(d => d.CategoryName, opt => opt.MapFrom(s => s.Category != null ? s.Category.Name : null));
    }
}