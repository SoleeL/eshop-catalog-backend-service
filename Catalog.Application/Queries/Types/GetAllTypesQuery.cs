using Catalog.Application.DTOs;
using MediatR;

namespace Catalog.Application.Queries.Types;

public abstract class GetAllTypesQuery : IRequest<IEnumerable<TypeResponseDto>>
{
}