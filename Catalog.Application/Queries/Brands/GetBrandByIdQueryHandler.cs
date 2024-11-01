using Catalog.Application.DTOs;
using Catalog.Application.Mappers;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Queries.Brands;

public class GetBrandByIdQueryHandler : IRequestHandler<GetBrandByIdQuery, BrandResponseDto?>
{
    private readonly IBrandRepository _brandRepository;

    public GetBrandByIdQueryHandler(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    public async Task<BrandResponseDto?> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
    {
        BrandEntity? brandEntity = await _brandRepository.GetByIdAsync(request.Id);

        if (brandEntity == null) return null;
        
        BrandResponseDto brandResponseDto = CatalogMapper.Mapper.Map<BrandResponseDto>(brandEntity);
        return brandResponseDto;
    }
}