namespace Catalog.Application.Dtos.Entities;

public class BaseDto<T>
{
    public T Id { get; set; }
    public long CreatedAt { get; set; }
    public long UpdatedAt { get; set; }
}