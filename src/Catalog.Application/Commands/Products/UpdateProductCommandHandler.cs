using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Commands.Products;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
{
    private readonly IProductRepository _productRepository;

    public UpdateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        ProductEntity? productEntity = await _productRepository.GetByIdAsync(request.Id);

        if (productEntity == null) return false;
        
        productEntity.Name = request.Name;

        await _productRepository.UpdateAsync(productEntity);
        return true;
    }
}