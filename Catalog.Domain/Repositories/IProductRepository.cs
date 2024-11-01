using Catalog.Domain.Entities;

namespace Catalog.Domain.Repositories;

public interface IProductRepository
{
    Task AddAsync(ProductEntity productEntity);
    
    Task<IEnumerable<ProductEntity>> GetAllAsync();
    Task<ProductEntity?> GetByIdAsync(Guid id);
    Task<IEnumerable<ProductEntity>> GetByNameAsync(string productName);
    
    Task<IEnumerable<ProductEntity>> GetByCategoryIdAsync(Guid categoryId);
    Task<IEnumerable<ProductEntity>> GetByCategoryNameAsync(string categoryName);
    
    Task<IEnumerable<ProductEntity>> GetByBrandIdAsync(Guid brandId);
    Task<IEnumerable<ProductEntity>> GetByBrandNameAsync(string brandName);
    
    Task<IEnumerable<ProductEntity>> GetByTypeIdAsync(Guid typeId);
    Task<IEnumerable<ProductEntity>> GetByTypeNameAsync(string typeName);
    
    Task UpdateAsync(ProductEntity product);
    Task DeleteAsync(Guid id);
}