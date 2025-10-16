using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TechECommercePlatform.Application.Common.Interfaces;

namespace TechECommercePlatform.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateProductCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("Product name is required.")
            .MaximumLength(200).WithMessage("Product name must not exceed 200 characters.")
            .MustAsync(BeUniqueName).WithMessage("Product name already exists.");

        RuleFor(v => v.SKU)
            .NotEmpty().WithMessage("SKU is required.")
            .MaximumLength(50).WithMessage("SKU must not exceed 50 characters.")
            .MustAsync(BeUniqueSKU).WithMessage("SKU already exists.");

        RuleFor(v => v.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0.");

        RuleFor(v => v.CategoryId)
            .GreaterThan(0).WithMessage("Category is required.")
            .MustAsync(CategoryExists).WithMessage("Category does not exist.");

        RuleFor(v => v.Description)
            .MaximumLength(2000).WithMessage("Description must not exceed 2000 characters.")
            .When(v => !string.IsNullOrEmpty(v.Description));

        RuleFor(v => v.ImageUrl)
            .MaximumLength(500).WithMessage("Image URL must not exceed 500 characters.")
            .When(v => !string.IsNullOrEmpty(v.ImageUrl));
    }

    private async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
    {
        return !await _context.Products
            .AnyAsync(p => p.Name == name, cancellationToken);
    }

    private async Task<bool> BeUniqueSKU(string sku, CancellationToken cancellationToken)
    {
        return !await _context.Products
            .AnyAsync(p => p.SKU == sku, cancellationToken);
    }

    private async Task<bool> CategoryExists(int categoryId, CancellationToken cancellationToken)
    {
        return await _context.Categories
            .AnyAsync(c => c.Id == categoryId, cancellationToken);
    }
}