using MediatR;
using TechECommercePlatform.Application.Features.Products.DTOs;

namespace TechECommercePlatform.Application.Features.Products.Queries.GetProducts;

public record GetProductsQuery : IRequest<List<ProductDto>>
{
    public int? CategoryId { get; init; }
    public string? SearchTerm { get; init; }
    public bool? IsActive { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}