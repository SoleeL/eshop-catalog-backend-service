namespace Catalog.Application.Exceptions;

public class InvalidEntityException : Exception
{
    public InvalidEntityException(string message) : base(message) { }
}