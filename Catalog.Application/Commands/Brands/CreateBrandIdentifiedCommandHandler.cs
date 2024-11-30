using Catalog.Application.Dtos;
using Catalog.Application.Dtos.Entities;
using Catalog.Application.Idempotency;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.Commands.Brands;

public class CreateBrandIdentifiedCommandHandler : IdentifiedCommandHandler<CreateBrandCommand, BaseResponseDto<BrandDto>>
{
    public CreateBrandIdentifiedCommandHandler(
        IMediator mediator,
        IRequestManager requestManager,
        ILogger<IdentifiedCommandHandler<CreateBrandCommand, BaseResponseDto<BrandDto>>> logger)
        : base(mediator, requestManager, logger)
    {
    }

    protected override BaseResponseDto<BrandDto> CreateResultForDuplicateRequest()
    {
        // TODO
        return new BaseResponseDto<BrandDto>(null); // Ignore duplicate requests for creating order.
    }
}