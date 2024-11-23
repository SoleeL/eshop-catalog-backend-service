using Catalog.Application.Dtos.Entities;
using Catalog.Application.Mappers;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Queries.Products;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductResponseDto?>
{
    private readonly IProductRepository _productRepository;

    public GetProductByIdQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductResponseDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        ProductEntity? productEntity = await _productRepository.GetByIdAsync(request.Id);

        if (productEntity == null) return null;
        
        ProductResponseDto productResponseDto = CatalogMapper.Mapper.Map<ProductResponseDto>(productEntity);
        return productResponseDto;
    }
}