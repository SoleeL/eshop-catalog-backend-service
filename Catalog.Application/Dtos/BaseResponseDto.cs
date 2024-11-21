using System.Text.Json.Serialization;
using FluentValidation.Results;

namespace Catalog.Application.Dtos;

public class BaseResponseDto<T>
{
    public bool Succcess { get; set; } = true;
    public T? Data { get; set; }
    public string? Message { get; set; }
    
    [JsonIgnore] // Este atributo evitará que la propiedad sea serializada en JSON
    public int TotalItemCount { get; set; }
}