using MediatR;
using TechECommercePlatform.Application.Features.Products.DTOs;

namespace TechECommercePlatform.Application.Features.Products.Commands.CreateProduct;

public record CreateProductCommand : IRequest<ProductDto>
{
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public decimal Price { get; init; }
    public string SKU { get; init; } = string.Empty;
    public string? ImageUrl { get; init; }
    public int CategoryId { get; init; }
}