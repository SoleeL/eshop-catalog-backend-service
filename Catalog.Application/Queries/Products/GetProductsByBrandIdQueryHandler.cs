using Catalog.Application.DTOs;
using Catalog.Application.Mappers;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Queries.Products;

public class GetProductsByBrandIdQueryHandler : IRequestHandler<GetProductsByBrandIdQuery, IEnumerable<ProductResponseDto>>
{
    private readonly IProductRepository _productRepository;

    public GetProductsByBrandIdQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<ProductResponseDto>> Handle(GetProductsByBrandIdQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<ProductEntity> productEntities = await _productRepository.GetByBrandIdAsync(request.Id);
        IEnumerable<ProductResponseDto> productResponseDtos = CatalogMapper.Mapper
            .Map<IEnumerable<ProductResponseDto>>(productEntities);
        return productResponseDtos;
    }
}