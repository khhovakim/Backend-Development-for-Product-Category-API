namespace ProductCategoryApi.Application.DTOs;

public record ProductCreateDto(string ProductName, decimal ProductPrice, List<int> CategoryIds);
public record ProductUpdateDto(string ProductName, decimal ProductPrice, List<int> CategoryIds);
public record ProductReadDto(int ProductId, string ProductName, decimal ProductPrice, List<CategoryReadDto> Categories);