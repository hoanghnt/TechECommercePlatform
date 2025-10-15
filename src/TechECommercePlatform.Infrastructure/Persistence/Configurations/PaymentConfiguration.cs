using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechECommercePlatform.Domain.Entities;

namespace TechECommercePlatform.Infrastructure.Persistence.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payments");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.OrderId)
            .IsRequired();

        builder.HasIndex(p => p.OrderId)
            .IsUnique();

        builder.Property(p => p.Amount)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(p => p.Status)
            .IsRequired()
            .HasConversion<int>();

        builder.HasIndex(p => p.Status);

        builder.Property(p => p.PaymentMethod)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(p => p.TransactionId)
            .HasMaxLength(200);

        builder.Property(p => p.PaymentDate);

        builder.HasOne(p => p.Order)
            .WithOne(p => p.Payment)
            .HasForeignKey<Payment>(p => p.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(p => p.DomainEvents);
    }
}