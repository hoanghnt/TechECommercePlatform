using Microsoft.EntityFrameworkCore;
using TechECommercePlatform.Domain.Entities;

namespace TechECommercePlatform.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Category> Categories { get; }
    DbSet<Product> Products { get; }
    DbSet<Inventory> Inventories { get; }
    DbSet<ShoppingCart> ShoppingCarts { get; }
    DbSet<ShoppingCartItem> ShoppingCartItems { get; }
    DbSet<Order> Orders { get; }
    DbSet<OrderItem> OrderItems { get; }
    DbSet<Payment> Payments { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}