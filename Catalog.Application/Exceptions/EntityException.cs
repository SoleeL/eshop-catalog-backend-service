namespace Catalog.Application.Exceptions;

public class EntityException : Exception
{
    public EntityException(string message) : base(message) { }
}