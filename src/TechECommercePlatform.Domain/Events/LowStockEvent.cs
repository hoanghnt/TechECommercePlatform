using TechECommercePlatform.Domain.Entities.Common;

namespace TechECommercePlatform.Domain.Events;

public class LowStockEvent : IDomainEvent
{
    public int ProductId { get; }
    public int CurrentQuantity { get; }
    public DateTime OccurredOn { get; }

    public LowStockEvent(int productId, int currentQuantity)
    {
        ProductId = productId;
        CurrentQuantity = currentQuantity;
        OccurredOn = DateTime.UtcNow;
    }
}