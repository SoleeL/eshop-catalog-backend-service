using Catalog.Application.Commands.Brands;
using Catalog.Application.DTOs;
using Catalog.Application.DTOs.Bases;
using MediatR;

namespace Catalog.API.Extensions;

public static class MediatorExtension
{
    public static void AddMediatorServices(this IHostApplicationBuilder builder)
    {
        // Agregar el mediador de los comandos y queries (CQRS)
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(Program)));
        
        // Agregar interpretes
        builder.Services.AddTransient<IRequestHandler<CreateBrandCommand, BaseResponseDto<BrandResponseDto>>, CreateBrandCommandHandler>();
    }
}