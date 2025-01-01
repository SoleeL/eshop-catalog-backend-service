namespace Catalog.Application.Dtos;

public class ValidationErrorDto
{
    public string? PropertyName { get; set; }
    public string? ErrorMessage { get; set; }
    public object? AttemptedValue { get; set; }
}