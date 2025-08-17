namespace ProductCategoryApi.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using ProductCategoryApi.Application.DTOs;
using ProductCategoryApi.Application.Services;

[ApiController]
[Route("api/products")]
public class ProductsController(ProductService service) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<ProductReadDto>> Create(ProductCreateDto dto, CancellationToken ct)
        => Ok(await service.CreateAsync(dto, ct));

    [HttpGet]
    public async Task<ActionResult<object>> List([FromQuery]int page = 1, [FromQuery]int pageSize = 10, CancellationToken ct = default)
    {
        var (items, total) = await service.PageAsync(page, pageSize, ct);
        return Ok(new { total, page, pageSize, items });
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductReadDto>> Get(int id, CancellationToken ct)
        => (await service.GetAsync(id, ct)) is { } p ? Ok(p) : NotFound();

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, ProductUpdateDto dto, CancellationToken ct)
        => await service.UpdateAsync(id, dto, ct) ? NoContent() : NotFound();

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
        => await service.DeleteAsync(id, ct) ? NoContent() : NotFound();
}
