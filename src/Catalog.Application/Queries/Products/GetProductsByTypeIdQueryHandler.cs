using Catalog.Application.Dtos.Entities;
using Catalog.Application.Mappers;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Queries.Products;

public class GetProductsByTypeIdQueryHandler : IRequestHandler<GetProductsByTypeIdQuery, IEnumerable<ProductResponseDto>>
{
    private readonly IProductRepository _productRepository;

    public GetProductsByTypeIdQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<ProductResponseDto>> Handle(GetProductsByTypeIdQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<ProductEntity> productEntities = await _productRepository.GetByTypeIdAsync(request.Id);
        IEnumerable<ProductResponseDto> productResponseDtos = CatalogMapper.Mapper
            .Map<IEnumerable<ProductResponseDto>>(productEntities);
        return productResponseDtos;
    }
}