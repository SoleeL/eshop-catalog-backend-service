using Catalog.Application.Dtos.Entities;
using Catalog.Application.Mappers;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Queries.Brands;

public abstract class GetBrandByIdQuery : IRequest<BrandResponseDto>
{
    public Guid Id { get; set; }

    public GetBrandByIdQuery(Guid id)
    {
        Id = id;
    }
}

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