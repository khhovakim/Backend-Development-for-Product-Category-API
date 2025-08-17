namespace ProductCategoryApi.Application.Abstractions;

using ProductCategoryApi.Domain.Entities;

public interface IProductRepository
{
    Task<Product> AddAsync(Product p, IEnumerable<int> categoryIds, CancellationToken ct);
    Task<Product?> GetWithCategoriesAsync(int id, CancellationToken ct);
    IQueryable<Product> QueryWithCategories();
    Task UpdateAsync(Product p, IEnumerable<int> categoryIds, CancellationToken ct);
    Task DeleteAsync(Product p, CancellationToken ct);
    Task<bool> CategoriesExistAsync(IEnumerable<int> categoryIds, CancellationToken ct);
}
