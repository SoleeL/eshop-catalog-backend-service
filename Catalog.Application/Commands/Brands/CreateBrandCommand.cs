using Catalog.Application.DTOs;
using Catalog.Application.DTOs.Bases;
using MediatR;

namespace Catalog.Application.Commands.Brands;

public class CreateBrandCommand : IRequest<BaseResponseDto<BrandResponseDto>>
{
    public string Name { get; set; }
    
    public CreateBrandCommand(string name)
    {
        Name = name;
    }
}