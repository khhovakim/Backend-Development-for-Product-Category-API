namespace ProductCategoryApi.Application.DTOs;

public record CategoryCreateDto(string CategoryName);
public record CategoryUpdateDto(string CategoryName);
public record CategoryReadDto(int CategoryId, string CategoryName);
