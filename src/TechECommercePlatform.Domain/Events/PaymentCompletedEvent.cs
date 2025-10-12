using TechECommercePlatform.Domain.Entities.Common;

namespace TechECommercePlatform.Domain.Events;

public class PaymentCompletedEvent : IDomainEvent
{
    public int PaymentId { get; }
    public int OrderId { get; }
    public decimal Amount { get; }
    public string TransactionId { get; }
    public DateTime OccurredOn { get; }

    public PaymentCompletedEvent(int paymentId, int orderId, decimal amount, string transactionId)
    {
        PaymentId = paymentId;
        OrderId = orderId;
        Amount = amount;
        TransactionId = transactionId;
        OccurredOn = DateTime.UtcNow;
    }
}