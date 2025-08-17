namespace ProductCategoryApi.Application.Validation;

using FluentValidation;
using ProductCategoryApi.Application.DTOs;

public class CategoryCreateValidator : AbstractValidator<CategoryCreateDto>
{
    public CategoryCreateValidator()
    {
        RuleFor(category => category.CategoryName)
            .NotEmpty()
            .MaximumLength(100);
    }
}

public class CategoryUpdateValidator : AbstractValidator<CategoryUpdateDto>
{
    public CategoryUpdateValidator()
    {
        RuleFor(category => category.CategoryName)
            .NotEmpty()
            .MaximumLength(100);
    }
}