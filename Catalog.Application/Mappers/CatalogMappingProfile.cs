using AutoMapper;
using Catalog.Application.Commands.Brands;
using Catalog.Application.Dtos.Entities;
using Catalog.Domain.Entities;

namespace Catalog.Application.Mappers;

public class CatalogMappingProfile : Profile
{
    public CatalogMappingProfile()
    {
        CreateMap<BaseEntity, BrandResponseDto>()
            .ForMember(
                destinationMember => destinationMember.Id,
                memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.Id.ToString())
            )
            .ForMember(
                destinationMember => destinationMember.CreatedAt,
                memberOptions => memberOptions.MapFrom(sourceMember => new DateTimeOffset(sourceMember.CreatedAt).ToUnixTimeMilliseconds())
            )
            .ForMember(
                destinationMember => destinationMember.UpdatedAt,
                memberOptions => memberOptions.MapFrom(sourceMember => new DateTimeOffset(sourceMember.UpdatedAt).ToUnixTimeMilliseconds())
            );
        
        CreateMap<BrandEntity, BrandResponseDto>().IncludeBase<BaseEntity, BrandResponseDto>();
        // .ForMember(); // TODO: AQUI ME QUEDE, HAY QUE PASAR EL Approval y limitar en la base de datos, quizas con una tabla de estados de aprovacion
        
        // Se puede simplificar el mapeo utilizando AutoMapper.
        // La siguiente configuración es suficiente para mapear las propiedades coincidentes entre CreateBrandCommand y BrandEntity.
        // Siempre que:
        //      - Los nombres de las propiedades coincidan (como Name y Description).
        //      - Las propiedades no coincidentes, como Enabled y Approval, se inicialicen de manera predeterminada en BrandEntity o más adelante en el flujo.
        CreateMap<CreateBrandCommand, BrandEntity>();
        
            // CreateMap<ProductEntity, ProductResponseDto>()
            //     .IncludeBase<BaseEntity, ProductResponseDto>();

            // CreateMap<BrandEntity, BrandResponseDto>()
            //     .ForMember(
            //         destinationMember => destinationMember.Id,
            //         memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.Id.ToString())
            //     )
            //     .ForMember(
            //         destinationMember => destinationMember.CreatedAt,
            //         memberOptions => memberOptions.MapFrom(sourceMember => new DateTimeOffset(sourceMember.CreatedAt).ToUnixTimeMilliseconds())
            //     )
            //     .ForMember(
            //         destinationMember => destinationMember.UpdatedAt,
            //         memberOptions => memberOptions.MapFrom(sourceMember => new DateTimeOffset(sourceMember.UpdatedAt).ToUnixTimeMilliseconds())
            //     );
            //
            // CreateMap<CategoryEntity, CategoryResponseDto>()
            //     .ForMember(
            //         destinationMember => destinationMember.Id,
            //         memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.Id.ToString())
            //     )
            //     .ForMember(
            //         destinationMember => destinationMember.CreatedAt,
            //         memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.CreatedAt.Millisecond)
            //     )
            //     .ForMember(
            //         destinationMember => destinationMember.UpdatedAt,
            //         memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.UpdatedAt.Millisecond)
            //     );
            //
            // CreateMap<ProductEntity, ProductResponseDto>()
            //     .ForMember(
            //         destinationMember => destinationMember.Id,
            //         memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.Id.ToString())
            //     )
            //     .ForMember(
            //         destinationMember => destinationMember.CreatedAt,
            //         memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.CreatedAt.Millisecond)
            //     )
            //     .ForMember(
            //         destinationMember => destinationMember.UpdatedAt,
            //         memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.UpdatedAt.Millisecond)
            //     );
            //
            // CreateMap<TypeEntity, TypeResponseDto>()
            //     .ForMember(
            //         destinationMember => destinationMember.Id,
            //         memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.Id.ToString())
            //     )
            //     .ForMember(
            //         destinationMember => destinationMember.CreatedAt,
            //         memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.CreatedAt.Millisecond)
            //     )
            //     .ForMember(
            //         destinationMember => destinationMember.UpdatedAt,
            //         memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.UpdatedAt.Millisecond)
            //     );

            // CreateMap<Product, ProductEntity>();

            // CreateMap<TypeEntity, TypeResponse>().ReverseMap();
            // CreateMap<BrandEntity, BrandResponse>().ReverseMap(); 
            // CreateMap<ProductEntity, ProductResponse>().ReverseMap();
            //
            // CreateMap<Pagination<Product>, Pagination<ProductResponse>>().ReverseMap();
    }
}