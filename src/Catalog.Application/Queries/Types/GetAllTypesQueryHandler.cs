using Catalog.Application.Dtos.Entities;
using Catalog.Application.Mappers;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Queries.Types;

public class GetAllTypesQueryHandler : IRequestHandler<GetAllTypesQuery, IEnumerable<TypeResponseDto>>
{
    private readonly ITypeRepository _typeRepository;

    public GetAllTypesQueryHandler(ITypeRepository typeRepository)
    {
        _typeRepository = typeRepository;
    }

    public async Task<IEnumerable<TypeResponseDto>> Handle(
        GetAllTypesQuery request,
        CancellationToken cancellationToken
    )
    {
        IEnumerable<TypeEntity> typeEntities = await _typeRepository.GetAllAsync();
        IEnumerable<TypeResponseDto> brandResponseDtos = CatalogMapper.Mapper.Map<IEnumerable<TypeResponseDto>>(typeEntities);
        return brandResponseDtos;
    }
}