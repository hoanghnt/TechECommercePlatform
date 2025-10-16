using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TechECommercePlatform.Application.Common.Interfaces;
using TechECommercePlatform.Application.Features.Products.DTOs;
using TechECommercePlatform.Domain.Entities;

namespace TechECommercePlatform.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = Product.Create(
            name: request.Name,
            description: request.Description,
            price: request.Price,
            sku: request.SKU,
            categoryId: request.CategoryId,
            imageUrl: request.ImageUrl
        );

        _context.Products.Add(product);
        await _context.SaveChangesAsync(cancellationToken);
        var productWithCategory = await _context.Products
            .Include(p => p.Category)
            .FirstAsync(p => p.Id == product.Id, cancellationToken);

        return _mapper.Map<ProductDto>(product);
    }
}