namespace ProductCategoryApi.Application.Validation;

using FluentValidation;
using ProductCategoryApi.Application.DTOs;

public class ProductCreateValidator : AbstractValidator<ProductCreateDto>
{
    private const string CategoryCountErrorMessage
        = "A product must be assigned to exactly 2 or 3 distinct categories.";
    private static bool HasTwoOrThreeDistinctIds(IReadOnlyCollection<int> ids) =>
        ids.Distinct().Count() is 2 or 3;

    public ProductCreateValidator()
    {
        RuleFor(product => product.ProductName)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(product => product.ProductPrice).GreaterThan(0);

        RuleFor(product => product.CategoryIds)
            .NotNull()
            .Must(HasTwoOrThreeDistinctIds)
            .WithMessage(CategoryCountErrorMessage);
    }
}

public class ProductUpdateValidator : AbstractValidator<ProductUpdateDto>
{
    private const string CategoryCountErrorMessage
        = "A product must be assigned to exactly 2 or 3 distinct categories.";
    private static bool HasTwoOrThreeDistinctIds(IEnumerable<int> ids) =>
        ids.Distinct().Count() is 2 or 3;
    public ProductUpdateValidator()
    {
        RuleFor(product => product.ProductName)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(product => product.ProductPrice).GreaterThan(0);

        RuleFor(product => product.CategoryIds)
            .NotNull()
            .Must(HasTwoOrThreeDistinctIds)
            .WithMessage(CategoryCountErrorMessage);
    }
}