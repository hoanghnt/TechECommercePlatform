using TechECommercePlatform.Domain.Entities.Common;
using TechECommercePlatform.Domain.Events;
using TechECommercePlatform.Domain.Exceptions;

namespace TechECommercePlatform.Domain.Entities;

public class Inventory : BaseAuditableEntity
{
    //Properties
    public int ProductId { get; private set; }

    public int Quantity { get; private set; }

    public int ReservedQuantity { get; private set; }

    public int AvailableQuantity => Quantity - ReservedQuantity;

    public Product? Product { get; private set; }

    private Inventory()
    {
    }

    public static Inventory Create(
        int productId,
        int initialQuantity)
    {
        if (productId <= 0)
            throw new ArgumentException("Invalid product ID", nameof(productId));
        
        if (initialQuantity < 0)
            throw new ArgumentException("Initial quantity cannot be negative", nameof(initialQuantity));
        
        return new Inventory
        {
            ProductId = productId,
            Quantity = initialQuantity
        };
    }

    public void AddStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero", nameof(quantity));
        Quantity += quantity;
    }

    public void Reserve(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero", nameof(quantity));
        
        if (quantity > AvailableQuantity)
            throw new InsufficientStockException(ProductId, quantity, AvailableQuantity);
        
        ReservedQuantity += quantity;
        
        if (AvailableQuantity < 10)  // threshold = 10
        {
            AddDomainEvent(new LowStockEvent(ProductId, AvailableQuantity));
        }
    }

    public void Release(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero", nameof(quantity));

        if (quantity > ReservedQuantity)
            throw new InvalidOperationException($"Cannot release {quantity}. Only {ReservedQuantity} reserved.");        
        ReservedQuantity -= quantity;
    }

    public void Deduct(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero", nameof(quantity));

        if (quantity > Quantity)
            throw new InvalidOperationException($"Cannot deduct {quantity}. Only {Quantity} in stock.");

        Quantity -= quantity;

        if (ReservedQuantity >= quantity)
            ReservedQuantity -= quantity;
        else
            ReservedQuantity = 0;
    }
}