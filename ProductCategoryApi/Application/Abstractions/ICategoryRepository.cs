namespace ProductCategoryApi.Application.Abstractions;

using ProductCategoryApi.Domain.Entities;

public interface ICategoryRepository
{
    Task<Category> AddAsync(Category c, CancellationToken ct);
    Task<Category?> GetAsync(int id, CancellationToken ct);
    IQueryable<Category> Query();
    Task UpdateAsync(Category c, CancellationToken ct);
    Task DeleteAsync(Category c, CancellationToken ct);
    Task<bool> ExistsAsync(int id, CancellationToken ct);
}