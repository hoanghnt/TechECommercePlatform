using TechECommercePlatform.Domain.Entities.Common;

namespace TechECommercePlatform.Domain.Entities;

public class OrderItem : BaseEntity
{
    public int OrderId { get; private set; }
    public int ProductId { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    
    public decimal Subtotal => Quantity * UnitPrice;

    public Order? Order { get; private set; }
    public Product? Product { get; private set; }

    private OrderItem()
    {
    }

    public static OrderItem Create(int orderId, int productId, int quantity, decimal unitPrice)
    {
        if (orderId <= 0)
            throw new ArgumentException("Invalid order ID", nameof(orderId));
        
        if (productId <= 0)
            throw new ArgumentException("Invalid product ID", nameof(productId));
        
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero", nameof(quantity));
        
        if (unitPrice <= 0)
            throw new ArgumentException("Unit price must be greater than zero", nameof(unitPrice));
        
        return new OrderItem
        {   
            OrderId = orderId,
            ProductId = productId,
            Quantity = quantity,
            UnitPrice = unitPrice
        };
    }
}