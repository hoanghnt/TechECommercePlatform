using MediatR;
using Microsoft.EntityFrameworkCore;
using TechECommercePlatform.Application.Common.Interfaces;
using TechECommercePlatform.Domain.Exceptions;

namespace TechECommercePlatform.Application.Features.Products.Commands.DeleteProduct;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public DeleteProductCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (product == null)
            throw new ProductNotFoundException(request.Id);

        product.Deactivate();
        
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}