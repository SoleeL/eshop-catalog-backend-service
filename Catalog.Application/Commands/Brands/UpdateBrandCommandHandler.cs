using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Commands.Brands;

public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, bool>
{
    private readonly IBrandRepository _brandRepository;

    public UpdateBrandCommandHandler(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    public async Task<bool> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
    {
        BrandEntity? brandEntity = await _brandRepository.GetByIdAsync(request.Id);

        if (brandEntity == null) return false;
        
        brandEntity.Name = request.Name;

        await _brandRepository.UpdateAsync(brandEntity);
        return true;
    }
}