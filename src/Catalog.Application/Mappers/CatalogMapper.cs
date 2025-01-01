using System.Reflection;
using AutoMapper;

namespace Catalog.Application.Mappers;

public static class CatalogMapper
{
    public static IMapper Mapper => Lazy.Value;

    private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(GetIMapper);

    private static IMapper GetIMapper()
    {
        var config = new MapperConfiguration(GetConfigurationAction);
        var mapper = config.CreateMapper();
        return mapper;
    }

    private static void GetConfigurationAction(IMapperConfigurationExpression configurationExpression)
    {
        configurationExpression.ShouldMapProperty = SetShouldMapProperty;
        configurationExpression.AddProfile<CatalogMappingProfile>();
    }

    private static bool SetShouldMapProperty(PropertyInfo propertyInfo)
    {
        if (propertyInfo.GetMethod == null) return false;
        return propertyInfo.GetMethod.IsPublic || propertyInfo.GetMethod.IsAssembly;
    }
}