using Catalog.Application.DTOs;
using Catalog.Application.Mappers;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Queries.Products;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductResponseDto>>
{
    private readonly IProductRepository _productRepository;

    public GetAllProductsQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<ProductResponseDto>> Handle(
        GetAllProductsQuery request,
        CancellationToken cancellationToken
    )
    {
        IEnumerable<ProductEntity> productEntities = await _productRepository.GetAllAsync();
        IEnumerable<ProductResponseDto> productResponseDtos = CatalogMapper.Mapper
            .Map<IEnumerable<ProductResponseDto>>(productEntities);
        return productResponseDtos;
    }
}