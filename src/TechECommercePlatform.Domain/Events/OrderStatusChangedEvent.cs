﻿using TechECommercePlatform.Domain.Entities.Common;
using TechECommercePlatform.Domain.Enums;

namespace TechECommercePlatform.Domain.Events;

public class OrderStatusChangedEvent : IDomainEvent
{
    public int OrderId { get; }
    public OrderStatus OldStatus { get; }
    public OrderStatus NewStatus { get; }
    public DateTime OccurredOn { get; }

    public OrderStatusChangedEvent(int orderId, OrderStatus oldStatus, OrderStatus newStatus)
    {
        OrderId = orderId;
        OldStatus = oldStatus;
        NewStatus = newStatus;
        OccurredOn = DateTime.UtcNow;
    }
}