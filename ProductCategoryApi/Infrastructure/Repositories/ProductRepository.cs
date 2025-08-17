namespace ProductCategoryApi.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using ProductCategoryApi.Domain.Entities;
using ProductCategoryApi.Infrastructure;
using ProductCategoryApi.Application.Abstractions;

public class ProductRepository(AppDbContext db) : IProductRepository
{
    public async Task<Product> AddAsync(Product p, IEnumerable<int> categoryIds, CancellationToken ct)
    {
        db.Products.Add(p);
        foreach (var cid in categoryIds.Distinct())
            db.ProductCategories.Add(new ProductCategory { Product = p, CategoryId = cid });

        await db.SaveChangesAsync(ct);
        return p;
    }

    public Task<Product?> GetWithCategoriesAsync(int id, CancellationToken ct) =>
        db.Products.Include(p => p.ProductCategories).ThenInclude(pc => pc.Category)
           .FirstOrDefaultAsync(p => p.Id == id, ct);

    public IQueryable<Product> QueryWithCategories() =>
        db.Products.Include(p => p.ProductCategories).ThenInclude(pc => pc.Category).AsNoTracking();

    public async Task UpdateAsync(Product p, IEnumerable<int> categoryIds, CancellationToken ct)
    {
        // update scalar fields
        db.Products.Update(p);

        // sync categories
        var current = db.ProductCategories.Where(pc => pc.ProductId == p.Id);
        db.ProductCategories.RemoveRange(current);

        foreach (var cid in categoryIds.Distinct())
            db.ProductCategories.Add(new ProductCategory { ProductId = p.Id, CategoryId = cid });

        await db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Product p, CancellationToken ct)
    {
        db.Products.Remove(p);
        await db.SaveChangesAsync(ct);
    }

    public Task<bool> CategoriesExistAsync(IEnumerable<int> ids, CancellationToken ct) =>
        db.Categories.CountAsync(c => ids.Contains(c.Id), ct).ContinueWith(t => t.Result == ids.Distinct().Count(), ct);
}
