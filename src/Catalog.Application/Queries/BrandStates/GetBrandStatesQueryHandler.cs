using Catalog.Application.Dtos;
using Catalog.Application.Dtos.Entities;
using Catalog.Application.Mappers;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Queries.BrandStates;

public record GetBrandStatesQuery : IRequest<BaseResponseDto<IEnumerable<BrandStateDto>>>;

public class GetBrandStatesQueryHandler
    : IRequestHandler<GetBrandStatesQuery, BaseResponseDto<IEnumerable<BrandStateDto>>>
{
    private readonly IBrandStateRepository _brandStateRepository;

    public GetBrandStatesQueryHandler(IBrandStateRepository brandStateRepository)
    {
        _brandStateRepository = brandStateRepository;
    }

    public async Task<BaseResponseDto<IEnumerable<BrandStateDto>>> Handle(
        GetBrandStatesQuery request,
        CancellationToken cancellationToken
    )
    {
        IEnumerable<BrandStateEntity> brandStateEntities = await _brandStateRepository.GetAll(cancellationToken);
        IEnumerable<BrandStateDto> brandStateDtos = CatalogMapper.Mapper.Map<IEnumerable<BrandStateDto>>(
            brandStateEntities);

        BaseResponseDto<IEnumerable<BrandStateDto>> baseResponseDtos = new BaseResponseDto<IEnumerable<BrandStateDto>>(
            brandStateDtos);

        return baseResponseDtos;
    }
}