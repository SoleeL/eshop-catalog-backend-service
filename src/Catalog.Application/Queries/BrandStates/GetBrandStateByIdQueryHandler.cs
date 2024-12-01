using Catalog.Application.Dtos;
using Catalog.Application.Dtos.Entities;
using Catalog.Application.Mappers;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Queries.BrandStates;

public class GetBrandStateByIdQuery : IRequest<BaseResponseDto<BrandStateDto>>
{
    public int Id { get; set; }

    public GetBrandStateByIdQuery(int id)
    {
        Id = id;
    }
}

public class GetBrandStateByIdQueryHandler : IRequestHandler<GetBrandStateByIdQuery, BaseResponseDto<BrandStateDto>>
{
    private readonly IBrandStateRepository _brandStateRepository;

    public GetBrandStateByIdQueryHandler(IBrandStateRepository brandStateRepository)
    {
        _brandStateRepository = brandStateRepository;
    }

    public async Task<BaseResponseDto<BrandStateDto>> Handle(
        GetBrandStateByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        BrandStateEntity? brandStateEntity = await _brandStateRepository.GetById(request.Id, cancellationToken);

        BrandStateDto brandStateDto = CatalogMapper.Mapper.Map<BrandStateDto>(brandStateEntity);

        BaseResponseDto<BrandStateDto> baseDto = new BaseResponseDto<BrandStateDto>(brandStateDto);

        if (brandStateEntity == null)
        {
            baseDto.Succcess = false;
            baseDto.Message = "Brand state does not exist";
        }

        return baseDto;
    }
}