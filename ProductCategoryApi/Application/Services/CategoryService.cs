namespace ProductCategoryApi.Application.Services;

using Microsoft.EntityFrameworkCore;
using ProductCategoryApi.Application.DTOs;
using ProductCategoryApi.Domain.Entities;
using ProductCategoryApi.Application.Abstractions;

public class CategoryService(ICategoryRepository repo)
{
    public async Task<CategoryReadDto> CreateAsync(CategoryCreateDto dto, CancellationToken ct)
    {
        var c = await repo.AddAsync(new Category { Name = dto.CategoryName }, ct);
        return new CategoryReadDto(c.Id, c.Name);
    }

    public async Task<CategoryReadDto?> GetAsync(int id, CancellationToken ct)
    {
        var c = await repo.GetAsync(id, ct);
        return c is null ? null : new CategoryReadDto(c.Id, c.Name);
    }

    public async Task<bool> UpdateAsync(int id, CategoryUpdateDto dto, CancellationToken ct)
    {
        var c = await repo.GetAsync(id, ct);
        if (c is null) return false;
        c.Name = dto.CategoryName;
        await repo.UpdateAsync(c, ct);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct)
    {
        var c = await repo.GetAsync(id, ct);
        if (c is null) return false;
        await repo.DeleteAsync(c, ct);
        return true;
    }

    public async Task<(IEnumerable<CategoryReadDto> Items, int Total)> PageAsync(int page, int size, CancellationToken ct)
    {
        var q = repo.Query();
        var total = await q.CountAsync(ct);
        var items = await q.Skip((page-1)*size).Take(size).Select(x => new CategoryReadDto(x.Id, x.Name)).ToListAsync(ct);
        return (items, total);
    }
}
