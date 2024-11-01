using Catalog.Application.DTOs;
using MediatR;

namespace Catalog.Application.Queries.Types;

public abstract class GetTypeByIdQuery : IRequest<TypeResponseDto>
{
    public Guid Id { get; set; }

    public GetTypeByIdQuery(Guid id)
    {
        Id = id;
    }
}