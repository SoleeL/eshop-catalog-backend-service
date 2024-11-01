using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Commands.Types;

public class UpdateTypeCommandHandler : IRequestHandler<UpdateTypeCommand, bool>
{
    private readonly ITypeRepository _typeRepository;

    public UpdateTypeCommandHandler(ITypeRepository typeRepository)
    {
        _typeRepository = typeRepository;
    }

    public async Task<bool> Handle(UpdateTypeCommand request, CancellationToken cancellationToken)
    {
        TypeEntity? typeEntity = await _typeRepository.GetByIdAsync(request.Id);

        if (typeEntity == null) return false;
        
        typeEntity.Name = request.Name;

        await _typeRepository.UpdateAsync(typeEntity);
        return true;
    }
}