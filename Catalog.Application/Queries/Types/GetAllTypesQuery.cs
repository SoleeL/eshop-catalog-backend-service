using Catalog.Application.Dtos.Entities;
using MediatR;

namespace Catalog.Application.Queries.Types;

public abstract class GetAllTypesQuery : IRequest<IEnumerable<TypeResponseDto>>
{
}