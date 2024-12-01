using MediatR;

namespace Catalog.Application.Commands;

public class IdentifiedCommand<T, TR> : IRequest<TR>
    where T : IRequest<TR>
{
    public T Command { get; }
    public Guid Id { get; }
    
    public IdentifiedCommand(T command, Guid id)
    {
        Command = command;
        Id = id;
    }
}