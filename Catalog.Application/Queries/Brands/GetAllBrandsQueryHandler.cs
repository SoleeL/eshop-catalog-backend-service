using Catalog.Application.DTOs;
using Catalog.Application.Mappers;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Queries.Brands;

public class GetAllBrandsQueryHandler : IRequestHandler<GetAllBrandsQuery, IEnumerable<BrandResponseDto>>
{
    private readonly IBrandRepository _brandRepository;

    public GetAllBrandsQueryHandler(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    public async Task<IEnumerable<BrandResponseDto>> Handle(
        GetAllBrandsQuery request,
        CancellationToken cancellationToken
    )
    {
        IEnumerable<BrandEntity> brandEntities = await _brandRepository.GetAllAsync();
        IEnumerable<BrandResponseDto> brandResponseDtos = CatalogMapper.Mapper.Map<IEnumerable<BrandResponseDto>>(brandEntities);
        return brandResponseDtos;
    }
}