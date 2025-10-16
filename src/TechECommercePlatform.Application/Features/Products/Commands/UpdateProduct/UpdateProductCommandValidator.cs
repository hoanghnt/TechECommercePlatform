using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TechECommercePlatform.Application.Common.Interfaces;

namespace TechECommercePlatform.Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateProductCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Id)
            .GreaterThan(0).WithMessage("Product ID is required.");

        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("Product name is required.")
            .MaximumLength(200).WithMessage("Product name must not exceed 200 characters.")
            .MustAsync(BeUniqueName).WithMessage("Product name already exists.");

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

    private async Task<bool> BeUniqueName(UpdateProductCommand command, string name, CancellationToken cancellationToken)
    {
        return !await _context.Products
            .AnyAsync(p => p.Name == name && p.Id != command.Id, cancellationToken);
    }

    private async Task<bool> CategoryExists(int categoryId, CancellationToken cancellationToken)
    {
        return await _context.Categories
            .AnyAsync(c => c.Id == categoryId, cancellationToken);
    }
}