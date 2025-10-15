using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechECommercePlatform.Domain.Entities;

namespace TechECommercePlatform.Infrastructure.Persistence.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItems");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.OrderId)
            .IsRequired();

        builder.Property(o => o.ProductId)
            .IsRequired();

        builder.Property(o => o.Quantity)
            .IsRequired();

        builder.Property(o => o.UnitPrice)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.HasOne(o => o.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(o => o.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(o => o.Product)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(o => o.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(o => new { o.OrderId, o.ProductId })
            .IsUnique();

        builder.Ignore(o => o.Subtotal);
        builder.Ignore(o => o.DomainEvents);
    }
}