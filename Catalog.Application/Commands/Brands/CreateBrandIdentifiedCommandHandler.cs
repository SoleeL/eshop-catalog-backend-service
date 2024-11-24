using Catalog.Application.Dtos;
using Catalog.Application.Dtos.Entities;
using Catalog.Application.Idempotency;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.Commands.Brands;

public class CreateBrandIdentifiedCommandHandler : IdentifiedCommandHandler<CreateBrandCommand, BaseResponseDto<BrandResponseDto>>
{
    public CreateBrandIdentifiedCommandHandler(
        IMediator mediator,
        IRequestManager requestManager,
        ILogger<IdentifiedCommandHandler<CreateBrandCommand, BaseResponseDto<BrandResponseDto>>> logger)
        : base(mediator, requestManager, logger)
    {
    }

    protected override BaseResponseDto<BrandResponseDto> CreateResultForDuplicateRequest()
    {
        // TODO
        return new BaseResponseDto<BrandResponseDto>(null); // Ignore duplicate requests for creating order.
    }
}