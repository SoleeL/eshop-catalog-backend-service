using Catalog.Application.DTOs;
using Catalog.Application.Mappers;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Queries.Products;

public class GetProductsByNameQueryHandler : IRequestHandler<GetProductsByNameQuery, IEnumerable<ProductResponseDto>>
{
    private readonly IProductRepository _productRepository;

    public GetProductsByNameQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<ProductResponseDto>> Handle(
        GetProductsByNameQuery request,
        CancellationToken cancellationToken
    )
    {
        IEnumerable<ProductEntity> productEntities = await _productRepository.GetByNameAsync(request.Name);
        IEnumerable<ProductResponseDto> productResponseDtos = CatalogMapper.Mapper
            .Map<IEnumerable<ProductResponseDto>>(productEntities);
        return productResponseDtos;
    }
}