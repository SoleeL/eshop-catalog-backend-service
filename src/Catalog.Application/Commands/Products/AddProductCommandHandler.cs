using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Commands.Products;

public class AddProductCommandHandler : IRequestHandler<AddProductCommand, Guid>
{
    private readonly IProductRepository _productRepository;

    public AddProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Guid> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        ProductEntity productEntity = new ProductEntity { Name = request.Name };

        await _productRepository.AddAsync(productEntity);

        return productEntity.Id;
    }
}