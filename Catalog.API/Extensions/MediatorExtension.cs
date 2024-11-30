using System.Reflection;
using Catalog.Application.Behaviors;
using Catalog.Application.Commands.Brands;
using Catalog.Application.Dtos;
using Catalog.Application.Dtos.Entities;
using Catalog.Application.Queries.Brands;
using Catalog.Application.Validations;
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
        
        // Agregar validadores de los comandos y queries
        builder.Services.AddScoped<IValidator<CreateBrandCommand>, CreateBrandCommandValidator>(); // Query de validacion con repository como Scoped
        builder.Services.AddSingleton<IValidator<GetPageBrandsQuery>, GetPageBrandsQueryValidator>();
        
        // Agregar interpretes de los comandos y queries (CQRS)
        builder.Services.AddTransient<IRequestHandler<CreateBrandCommand, BaseResponseDto<BrandResponseDto>>, CreateBrandCommandHandler>();
        builder.Services.AddTransient<IRequestHandler<GetPageBrandsQuery, BaseResponseDto<IEnumerable<BrandResponseDto>>>, GetPageBrandsQueryHandler>();
        builder.Services.AddTransient<IRequestHandler<GetBrandByIdQuery, BaseResponseDto<BrandResponseDto>>, GetBrandByIdQueryHandler>();
        // builder.Services.AddTransient<IRequestHandler<GetBrandByIdQuery, BaseResponseDto<BrandResponseDto>>, GetBrandByIdQueryHandler>();
        builder.Services.AddTransient<IRequestHandler<DeleteBrandCommand, BaseResponseDto<BrandResponseDto>>, DeleteBrandCommandHandler>();
    }
}