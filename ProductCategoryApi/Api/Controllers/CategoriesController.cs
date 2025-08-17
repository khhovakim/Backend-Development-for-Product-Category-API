namespace ProductCategoryApi.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using ProductCategoryApi.Application.DTOs;
using ProductCategoryApi.Application.Services;

[ApiController]
[Route("api/categories")]
public class CategoriesController(CategoryService service) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<CategoryReadDto>> Create(CategoryCreateDto dto, CancellationToken ct)
        => Ok(await service.CreateAsync(dto, ct));

    [HttpGet]
    public async Task<ActionResult<object>> List([FromQuery]int page = 1, [FromQuery]int pageSize = 10, CancellationToken ct = default)
    {
        var (items, total) = await service.PageAsync(page, pageSize, ct);
        return Ok(new { total, page, pageSize, items });
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CategoryReadDto>> Get(int id, CancellationToken ct)
        => (await service.GetAsync(id, ct)) is { } c ? Ok(c) : NotFound();

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, CategoryUpdateDto dto, CancellationToken ct)
        => await service.UpdateAsync(id, dto, ct) ? NoContent() : NotFound();

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
        => await service.DeleteAsync(id, ct) ? NoContent() : NotFound();
}
