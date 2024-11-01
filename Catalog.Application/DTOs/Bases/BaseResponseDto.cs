namespace Catalog.Application.DTOs.Bases;

public class BaseResponseDto<T>
{
    public bool Succcess { get; set; } = true;
    public T? Data { get; set; }
    public string? Message { get; set; }
    public IEnumerable<BaseError>? Errors { get; set; }
    
    
}