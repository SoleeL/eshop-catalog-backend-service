using Catalog.Application.DTOs;
using Catalog.Application.Mappers;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Queries.Types;

public class GetTypeByIdQueryHandler : IRequestHandler<GetTypeByIdQuery, TypeResponseDto?>
{
    private readonly ITypeRepository _typeRepository;

    public GetTypeByIdQueryHandler(ITypeRepository typeRepository)
    {
        _typeRepository = typeRepository;
    }

    public async Task<TypeResponseDto?> Handle(GetTypeByIdQuery request, CancellationToken cancellationToken)
    {
        TypeEntity? typeEntity = await _typeRepository.GetByIdAsync(request.Id);

        if (typeEntity == null) return null;
        
        TypeResponseDto typeResponseDto = CatalogMapper.Mapper.Map<TypeResponseDto>(typeEntity);
        return typeResponseDto;
    }
}