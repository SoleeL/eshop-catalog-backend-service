using Catalog.Application.Dtos;
using Catalog.Application.Dtos.Entities;
using Catalog.Application.Mappers;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Commands.BrandStates;

public class DeleteBrandStateCommand : IRequest<BaseResponseDto<BrandStateDto>>
{
    public int Id { get; set; }

    public DeleteBrandStateCommand(int id)
    {
        Id = id;
    }
}

public class DeleteBrandStateCommandHandler : IRequestHandler<DeleteBrandStateCommand, BaseResponseDto<BrandStateDto>>
{
    private readonly IBrandStateRepository _brandStateRepository;

    public DeleteBrandStateCommandHandler(IBrandStateRepository brandStateRepository)
    {
        _brandStateRepository = brandStateRepository;
    }

    public async Task<BaseResponseDto<BrandStateDto>> Handle(
        DeleteBrandStateCommand request,
        CancellationToken cancellationToken
    )
    {
        BrandStateEntity? brandStateEntity = await _brandStateRepository.DeleteWithSaveChange(
            request.Id,
            cancellationToken);

        BrandStateDto brandStateDto = CatalogMapper.Mapper.Map<BrandStateDto>(brandStateEntity);

        BaseResponseDto<BrandStateDto> baseResponseDto = new BaseResponseDto<BrandStateDto>(brandStateDto);

        if (brandStateEntity == null)
        {
            baseResponseDto.Succcess = false;
            baseResponseDto.Message = "Brand state does not exist";
        }

        return baseResponseDto;
    }
}