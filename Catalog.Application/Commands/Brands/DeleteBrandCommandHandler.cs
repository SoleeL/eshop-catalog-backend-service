using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Commands.Brands;

public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, bool>
{
    private readonly IBrandRepository _brandRepository;

    public DeleteBrandCommandHandler(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }

    public async Task<bool> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
    {
        BrandEntity? brandEntity = await _brandRepository.GetByIdAsync(request.Id);

        if (brandEntity == null) return false;

        await _brandRepository.DeleteAsync(request.Id);
        return true;
    }
}