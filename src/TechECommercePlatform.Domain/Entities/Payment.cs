using TechECommercePlatform.Domain.Entities.Common;
using TechECommercePlatform.Domain.Enums;
using TechECommercePlatform.Domain.Events;
using TechECommercePlatform.Domain.Exceptions;

namespace TechECommercePlatform.Domain.Entities;

public class Payment : BaseAuditableEntity
{
    public int OrderId { get; private set; }
    public decimal Amount { get; private set; }
    public PaymentStatus Status { get; private set; }
    public PaymentMethod PaymentMethod { get; private set; }
    public string? TransactionId { get; private set; }
    
    public DateTime? PaymentDate { get; private set; }

    public Order? Order { get; private set; }

    private Payment()
    {
    }

    public static Payment Create(int orderId, decimal amount, PaymentMethod paymentMethod)
    {
        if (orderId <= 0)
            throw new ArgumentException("Invalid order ID", nameof(orderId));
        
        if (amount <= 0)
            throw new ArgumentException("Amount must be greater than zero", nameof(amount));
        
        return new Payment
        {
            OrderId = orderId,
            Amount = amount,
            PaymentMethod = paymentMethod,
            Status = PaymentStatus.Pending 
        };
    }
    
    public void MarkAsCompleted(string transactionId)
    {
        if (Status == PaymentStatus.Completed)
            throw new InvalidOperationException("Payment already completed");
        
        if (Status == PaymentStatus.Cancelled)
            throw new InvalidOperationException("Cannot complete cancelled payment");
        
        if (string.IsNullOrWhiteSpace(transactionId))
            throw new ArgumentException("Transaction ID cannot be empty", nameof(transactionId));
        
        Status = PaymentStatus.Completed;
        TransactionId = transactionId;
        PaymentDate = DateTime.UtcNow;
        
        AddDomainEvent(new PaymentCompletedEvent(Id, OrderId, Amount, transactionId));
    }
    
    public void MarkAsFailed(string reason)
    {
        if (Status == PaymentStatus.Completed)
            throw new InvalidOperationException("Cannot fail completed payment");
        
        if (Status == PaymentStatus.Cancelled)
            throw new InvalidOperationException("Payment already cancelled");
        
        Status = PaymentStatus.Failed;
    }
    
    public void InitiateRefund()
    {
        if (Status != PaymentStatus.Completed)
            throw new InvalidOperationException("Can only refund completed payments");
        
        Status = PaymentStatus.Refunded;
    }
    
    public void CompleteRefund(string refundTransactionId)
    {
        if (Status != PaymentStatus.Completed)
            throw new InvalidOperationException("Can only refund completed payments");
        
        if (string.IsNullOrWhiteSpace(refundTransactionId))
            throw new ArgumentException("Refund transaction ID cannot be empty", nameof(refundTransactionId));
        
        Status = PaymentStatus.Refunded;
        TransactionId = refundTransactionId;
    }
}