using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechECommercePlatform.Domain.Entities;

namespace TechECommercePlatform.Infrastructure.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.OrderNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(o => o.OrderNumber)
            .IsUnique();

        builder.Property(o => o.UserId)
            .IsRequired()
            .HasMaxLength(450);

        builder.HasIndex(o => o.UserId);
        
        builder.Property(o => o.OrderDate)
            .IsRequired();

        builder.HasIndex(o => o.OrderDate);
        
        builder.Property(o => o.Status)
            .IsRequired()
            .HasConversion<int>();

        builder.HasIndex(o => o.Status);
        
        builder.Property(o => o.TotalAmount)
            .IsRequired()
            .HasColumnType("decimal(18,2)");
        
        builder.Property(o => o.ShippingAddress)
            .IsRequired()
            .HasMaxLength(500);
        
        builder.HasMany(o => o.OrderItems)
            .WithOne(i => i.Order)
            .HasForeignKey(i => i.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(o => o.Payment)
            .WithOne(p => p.Order)
            .HasForeignKey<Payment>(p => p.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(o => o.DomainEvents);
    }
}