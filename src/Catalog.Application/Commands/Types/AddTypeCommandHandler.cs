using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Commands.Types;

public class AddTypeCommandHandler : IRequestHandler<AddTypeCommand, Guid>
{
    private readonly ITypeRepository _typeRepository;

    public AddTypeCommandHandler(ITypeRepository typeRepository)
    {
        _typeRepository = typeRepository;
    }

    public async Task<Guid> Handle(AddTypeCommand request, CancellationToken cancellationToken)
    {
        TypeEntity typeEntity = new TypeEntity { Name = request.Name };

        await _typeRepository.AddAsync(typeEntity);

        return typeEntity.Id;
    }
}