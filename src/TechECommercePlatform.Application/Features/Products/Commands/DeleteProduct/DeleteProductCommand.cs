using MediatR;

namespace TechECommercePlatform.Application.Features.Products.Commands.DeleteProduct;

public record DeleteProductCommand(int Id) : IRequest<Unit>;