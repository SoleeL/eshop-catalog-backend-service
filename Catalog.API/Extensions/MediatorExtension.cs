using System.Reflection;
using Catalog.Application.Behaviors;
using Catalog.Application.Commands.Brands;
using Catalog.Application.Dtos;
using Catalog.Application.Dtos.Entities;
using Catalog.Application.Queries.Brands;
using Catalog.Application.Validations;
using Catalog.Application.Validations.Utils;
using FluentValidation;
using MediatR;

namespace Catalog.API.Extensions;

public static class MediatorExtension
{
    public static void AddMediatorServices(this IHostApplicationBuilder builder)
    {
        // Agregar el mediador 
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining(typeof(Program));
            
            cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
            cfg.AddOpenBehavior(typeof(ValidatorBehavior<,>));
        });
        
        // Agregar validadores de los comandos y queries:
        
        // Query de validacion con repository como Scoped para contexto de base de datos
        builder.Services.AddScoped<IValidator<CreateBrandCommand>, CreateBrandCommandValidator>();
        builder.Services.AddSingleton<IValidator<GetPageBrandsQuery>, GetPageBrandsQueryValidator>();
        builder.Services.AddSingleton<IValidator<UpdateBrandCommand>, UpdateBrandCommandValidator>();
        
        // Agregar interpretes de los comandos y queries (CQRS)
        builder.Services.AddTransient<IRequestHandler<CreateBrandCommand, BaseResponseDto<BrandDto>>, CreateBrandCommandHandler>();
        builder.Services.AddTransient<IRequestHandler<GetPageBrandsQuery, BaseResponseDto<IEnumerable<BrandDto>>>, GetPageBrandsQueryHandler>();
        builder.Services.AddTransient<IRequestHandler<GetBrandByIdQuery, BaseResponseDto<BrandDto>>, GetBrandByIdQueryHandler>();
        builder.Services.AddTransient<IRequestHandler<UpdateBrandCommand, BaseResponseDto<BrandDto>>, UpdateBrandCommandHandler>();
        builder.Services.AddTransient<IRequestHandler<DeleteBrandCommand, BaseResponseDto<BrandDto>>, DeleteBrandCommandHandler>();
    }
}