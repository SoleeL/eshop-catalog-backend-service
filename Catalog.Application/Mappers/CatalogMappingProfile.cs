using AutoMapper;
using Catalog.Application.DTOs;
using Catalog.Domain.Entities;

namespace Catalog.Application.Mappers;

public class CatalogMappingProfile : Profile
{
    public CatalogMappingProfile()
    {
        CreateMap<BrandEntity, BrandResponseDto>()
            .ForMember(
                destinationMember => destinationMember.Id,
                memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.Id.ToString())
            )
            .ForMember(
                destinationMember => destinationMember.CreatedAt,
                memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.CreatedAt.Millisecond)
            )
            .ForMember(
                destinationMember => destinationMember.UpdatedAt,
                memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.UpdatedAt.Millisecond)
            );

        CreateMap<CategoryEntity, CategoryResponseDto>()
            .ForMember(
                destinationMember => destinationMember.Id,
                memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.Id.ToString())
            )
            .ForMember(
                destinationMember => destinationMember.CreatedAt,
                memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.CreatedAt.Millisecond)
            )
            .ForMember(
                destinationMember => destinationMember.UpdatedAt,
                memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.UpdatedAt.Millisecond)
            );

        CreateMap<ProductEntity, ProductResponseDto>()
            .ForMember(
                destinationMember => destinationMember.Id,
                memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.Id.ToString())
            )
            .ForMember(
                destinationMember => destinationMember.CreatedAt,
                memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.CreatedAt.Millisecond)
            )
            .ForMember(
                destinationMember => destinationMember.UpdatedAt,
                memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.UpdatedAt.Millisecond)
            );

        CreateMap<TypeEntity, TypeResponseDto>()
            .ForMember(
                destinationMember => destinationMember.Id,
                memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.Id.ToString())
            )
            .ForMember(
                destinationMember => destinationMember.CreatedAt,
                memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.CreatedAt.Millisecond)
            )
            .ForMember(
                destinationMember => destinationMember.UpdatedAt,
                memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.UpdatedAt.Millisecond)
            );

        // CreateMap<Product, ProductEntity>();

        // CreateMap<TypeEntity, TypeResponse>().ReverseMap();
        // CreateMap<BrandEntity, BrandResponse>().ReverseMap(); 
        // CreateMap<ProductEntity, ProductResponse>().ReverseMap();
        //
        // CreateMap<Pagination<Product>, Pagination<ProductResponse>>().ReverseMap();
    }
}