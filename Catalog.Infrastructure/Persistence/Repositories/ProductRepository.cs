using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;

namespace Catalog.Persistence.Repositories;

public class ProductRepository: IProductRepository
{
    public Task AddAsync(ProductEntity productEntity)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProductEntity>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ProductEntity?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProductEntity>> GetByNameAsync(string productName)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProductEntity>> GetByCategoryIdAsync(Guid categoryId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProductEntity>> GetByCategoryNameAsync(string categoryName)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProductEntity>> GetByBrandIdAsync(Guid brandId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProductEntity>> GetByBrandNameAsync(string brandName)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProductEntity>> GetByTypeIdAsync(Guid typeId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProductEntity>> GetByTypeNameAsync(string typeName)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(ProductEntity product)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}