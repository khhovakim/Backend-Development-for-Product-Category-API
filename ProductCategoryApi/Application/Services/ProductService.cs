namespace ProductCategoryApi.Application.Services;

using Microsoft.EntityFrameworkCore;
using ProductCategoryApi.Application.DTOs;
using ProductCategoryApi.Domain.Entities;
using ProductCategoryApi.Application.Abstractions;

public class ProductService(IProductRepository repo)
{
    public async Task<ProductReadDto?> GetAsync(int id, CancellationToken ct)
    {
        var p = await repo.GetWithCategoriesAsync(id, ct);
        return p is null ? null : Map(p);
    }

    public async Task<ProductReadDto> CreateAsync(ProductCreateDto dto, CancellationToken ct)
    {
        // validate categories exist
        if (!await repo.CategoriesExistAsync(dto.CategoryIds, ct))
            throw new ArgumentException("One or more category ids do not exist.");

        var p = await repo.AddAsync(new Product { Name = dto.ProductName, Price = dto.ProductPrice }, dto.CategoryIds, ct);
        var full = await repo.GetWithCategoriesAsync(p.Id, ct);
        return Map(full!);
    }

    public async Task<bool> UpdateAsync(int id, ProductUpdateDto dto, CancellationToken ct)
    {
        var p = await repo.GetWithCategoriesAsync(id, ct);
        if (p is null) return false;

        if (!await repo.CategoriesExistAsync(dto.CategoryIds, ct))
            throw new ArgumentException("One or more category ids do not exist.");

        p.Name = dto.ProductName;
        p.Price = dto.ProductPrice;
        await repo.UpdateAsync(p, dto.CategoryIds, ct);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct)
    {
        var p = await repo.GetWithCategoriesAsync(id, ct);
        if (p is null) return false;
        await repo.DeleteAsync(p, ct);
        return true;
    }

    public async Task<(IEnumerable<ProductReadDto> Items, int Total)> PageAsync(int page, int size, CancellationToken ct)
    {
        var q = repo.QueryWithCategories();
        var total = await q.CountAsync(ct);
        var items = await q.Skip((page-1)*size).Take(size)
            .Select(p => new ProductReadDto(
                p.Id, p.Name, p.Price,
                p.ProductCategories.Select(pc => new CategoryReadDto(pc.Category.Id, pc.Category.Name)).ToList()
            )).ToListAsync(ct);
        return (items, total);
    }

    private static ProductReadDto Map(Product p) =>
        new(p.Id, p.Name, p.Price,
            p.ProductCategories.Select(pc => new CategoryReadDto(pc.Category.Id, pc.Category.Name)).ToList());
}
