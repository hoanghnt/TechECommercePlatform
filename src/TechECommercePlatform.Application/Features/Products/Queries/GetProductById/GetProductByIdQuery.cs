using MediatR;
using TechECommercePlatform.Application.Features.Products.DTOs;

namespace TechECommercePlatform.Application.Features.Products.Queries.GetProductById;

public record GetProductByIdQuery(int Id) : IRequest<ProductDto>;