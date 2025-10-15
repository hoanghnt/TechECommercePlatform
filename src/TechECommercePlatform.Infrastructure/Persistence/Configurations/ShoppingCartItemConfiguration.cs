using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechECommercePlatform.Domain.Entities;

namespace TechECommercePlatform.Infrastructure.Persistence.Configurations;

public class ShoppingCartItemConfiguration : IEntityTypeConfiguration<ShoppingCartItem>
{
    public void Configure(EntityTypeBuilder<ShoppingCartItem> builder)
    {
        builder.ToTable("ShoppingCartItems");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.ShoppingCartId)
            .IsRequired();

        builder.Property(s => s.ProductId)
            .IsRequired();

        builder.HasIndex(s => new { s.ShoppingCartId, s.ProductId })
            .IsUnique();

        builder.Property(s => s.Quantity)
            .IsRequired()
            .HasDefaultValue(1);

        builder.HasOne(s => s.ShoppingCart)
            .WithMany(s => s.Items)
            .HasForeignKey(s => s.ShoppingCartId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(s => s.Product)
            .WithMany(p => p.ShoppingCartItems)
            .HasForeignKey(s => s.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Ignore(s => s.DomainEvents);
    }
}