using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TechECommercePlatform.Application.Common.Interfaces;
using TechECommercePlatform.Application.Features.Products.DTOs;
using TechECommercePlatform.Domain.Exceptions;

namespace TechECommercePlatform.Application.Features.Products.Queries.GetProductById;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (product == null)
            throw new ProductNotFoundException(request.Id);

        return _mapper.Map<ProductDto>(product);
    }
}