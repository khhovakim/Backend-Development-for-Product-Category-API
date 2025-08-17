namespace ProductCategoryApi.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using ProductCategoryApi.Domain.Entities;
using ProductCategoryApi.Infrastructure;
using ProductCategoryApi.Application.Abstractions;

public class CategoryRepository(AppDbContext db) : ICategoryRepository
{
    public async Task<Category> AddAsync(Category c, CancellationToken ct)
    {
        db.Categories.Add(c);
        await db.SaveChangesAsync(ct);
        return c;
    }

    public Task<Category?> GetAsync(int id, CancellationToken ct) =>
        db.Categories.FirstOrDefaultAsync(x => x.Id == id, ct);

    public IQueryable<Category> Query() => db.Categories.AsNoTracking();

    public async Task UpdateAsync(Category c, CancellationToken ct)
    {
        db.Categories.Update(c);
        await db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Category c, CancellationToken ct)
    {
        db.Categories.Remove(c);
        await db.SaveChangesAsync(ct);
    }

    public Task<bool> ExistsAsync(int id, CancellationToken ct) =>
        db.Categories.AnyAsync(x => x.Id == id, ct);
}
