using MediatR;
using Microsoft.EntityFrameworkCore;
using TechECommercePlatform.Application.Common.Interfaces;
using TechECommercePlatform.Domain.Exceptions;

namespace TechECommercePlatform.Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public UpdateProductCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (product == null)
            throw new ProductNotFoundException(request.Id);

        product.UpdateDetails(
            name: request.Name,
            description: request.Description,
            price: request.Price,
            imageUrl: request.ImageUrl
        );

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}