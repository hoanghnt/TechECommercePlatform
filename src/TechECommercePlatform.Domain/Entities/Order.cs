using System.Linq;
using TechECommercePlatform.Domain.Entities.Common;
using TechECommercePlatform.Domain.Enums;
using TechECommercePlatform.Domain.Events;
using TechECommercePlatform.Domain.Exceptions;

namespace TechECommercePlatform.Domain.Entities;

public class Order : BaseAuditableEntity
{
    public string OrderNumber { get; private set; } = string.Empty;
    public string UserId { get; private set; } = string.Empty;
    public decimal TotalAmount { get; private set; }
    public OrderStatus Status { get; private set; }
    public DateTime OrderDate { get; private set; }
    public string ShippingAddress { get; private set; } = string.Empty;

    public ICollection<OrderItem> OrderItems { get; private set; } = new List<OrderItem>();
    public Payment? Payment { get; private set; }

    private Order()
    {
    }

    public static Order Create(string orderNumber, string userId, string shippingAddress)
    {
        if (string.IsNullOrWhiteSpace(orderNumber))
            throw new ArgumentException("Order number cannot be empty", nameof(orderNumber));
    
        if (string.IsNullOrWhiteSpace(userId))
            throw new ArgumentException("User ID cannot be empty", nameof(userId));
    
        if (string.IsNullOrWhiteSpace(shippingAddress))
            throw new ArgumentException("Shipping address cannot be empty", nameof(shippingAddress));
        
        var order = new Order
        {
            OrderNumber = orderNumber,
            UserId = userId,
            ShippingAddress = shippingAddress,
            Status = OrderStatus.Pending,
            OrderDate = DateTime.UtcNow  
        };
        
        order.AddDomainEvent(new OrderCreatedEvent(order.Id, order.UserId, order.TotalAmount));

        return order;
    }

    public void AddItem(int productId, int quantity, decimal unitPrice)
    {
        if (productId <= 0)
            throw new ArgumentException("Invalid product ID", nameof(productId));
        
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero", nameof(quantity));

        if (unitPrice <= 0)
            throw new ArgumentException("Unit price must be greater than zero", nameof(unitPrice));
        
        if (Status != OrderStatus.Pending)
            throw new InvalidOperationException("Cannot add items to order that is not pending");
        
        var item = OrderItem.Create(Id, productId, quantity, unitPrice);
        OrderItems.Add(item);
        
        CalculateTotal();
    }
    
    private void CalculateTotal()
    {
        TotalAmount = OrderItems.Sum(item => item.Subtotal);
    }

    public void UpdateStatus(OrderStatus newStatus)
    {
        if (Status == newStatus)
            return;
        
        if (!IsValidStatusTransition(Status, newStatus))
            throw new InvalidOrderOperationException(
                $"Cannot transition from {Status} to {newStatus}");
        
        var oldStatus = Status;
        Status = newStatus;
        
        AddDomainEvent(new OrderStatusChangedEvent(Id, oldStatus, newStatus));
    }
    
    public void Cancel()
    {
        if (Status != OrderStatus.Pending && Status != OrderStatus.Processing)
            throw new InvalidOrderOperationException(
                $"Cannot cancel order with status {Status}");
        
        UpdateStatus(OrderStatus.Cancelled);
    }
    
    public void MarkAsProcessing()
    {
        if (Status != OrderStatus.Pending)
            throw new InvalidOrderOperationException("Only pending orders can be marked as processing");
        
        UpdateStatus(OrderStatus.Processing);
    }
    
    public void MarkAsShipped()
    {
        if (Status != OrderStatus.Processing)
            throw new InvalidOrderOperationException("Only processing orders can be marked as shipped");
        
        UpdateStatus(OrderStatus.Shipped);
    }
    
    public void MarkAsDelivered()
    {
        if (Status != OrderStatus.Shipped)
            throw new InvalidOrderOperationException("Only shipped orders can be marked as delivered");
        
        UpdateStatus(OrderStatus.Delivered);
    }
    
    private bool IsValidStatusTransition(OrderStatus from, OrderStatus to)
    {
        return (from, to) switch
        {
            (OrderStatus.Pending, OrderStatus.Processing) => true,
            (OrderStatus.Pending, OrderStatus.Cancelled) => true,
            (OrderStatus.Processing, OrderStatus.Shipped) => true,
            (OrderStatus.Processing, OrderStatus.Cancelled) => true,
            (OrderStatus.Shipped, OrderStatus.Delivered) => true,
            _ => false
        };
    }
}