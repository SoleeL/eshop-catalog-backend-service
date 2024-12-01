using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Commands.Types;

public class DeleteTypeCommandHandler : IRequestHandler<DeleteTypeCommand, bool>
{
    private readonly ITypeRepository _typeRepository;

    public DeleteTypeCommandHandler(ITypeRepository typeRepository)
    {
        _typeRepository = typeRepository;
    }

    public async Task<bool> Handle(DeleteTypeCommand request, CancellationToken cancellationToken)
    {
        TypeEntity? typeEntity = await _typeRepository.GetByIdAsync(request.Id);

        if (typeEntity == null) return false;

        await _typeRepository.DeleteAsync(request.Id);
        return true;
    }
}