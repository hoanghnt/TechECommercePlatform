using TechECommercePlatform.Domain.Entities.Common;

namespace TechECommercePlatform.Domain.Events;

public class OrderCreatedEvent : IDomainEvent
{
    public int OrderId { get; }
    public string UserId { get; }
    public decimal TotalAmount { get; }
    public DateTime OccurredOn { get; }
    
    public OrderCreatedEvent(int orderId, string userId, decimal totalAmount)
    {
        OrderId = orderId;
        UserId = userId;
        TotalAmount = totalAmount;
        OccurredOn = DateTime.UtcNow;
    }
}





