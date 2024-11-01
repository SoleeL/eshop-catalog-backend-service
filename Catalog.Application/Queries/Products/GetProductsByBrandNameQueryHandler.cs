using Catalog.Application.DTOs;
using Catalog.Application.Mappers;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Queries.Products;

public class GetProductByBrandNameQueryHandler : IRequestHandler<GetProductByBrandNameQuery, IEnumerable<ProductResponseDto>>
{
    private readonly IProductRepository _productRepository;

    public GetProductByBrandNameQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<ProductResponseDto>> Handle(GetProductByBrandNameQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<ProductEntity> productEntities = await _productRepository.GetByBrandNameAsync(request.Name);
        IEnumerable<ProductResponseDto> productResponseDtos = CatalogMapper.Mapper
            .Map<IEnumerable<ProductResponseDto>>(productEntities);
        return productResponseDtos;
    }
}