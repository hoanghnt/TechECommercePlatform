using MediatR;

namespace TechECommercePlatform.Application.Features.Products.Commands.UpdateProduct;

public record UpdateProductCommand : IRequest<Unit>
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public decimal Price { get; init; }
    public int CategoryId { get; init; }
    public string? ImageUrl { get; init; }
}