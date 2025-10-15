using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechECommercePlatform.Domain.Entities;

namespace TechECommercePlatform.Infrastructure.Persistence.Configurations;

public class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
{
    public void Configure(EntityTypeBuilder<ShoppingCart> builder)
    {
        builder.ToTable("ShoppingCarts");
        
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.UserId)
            .IsRequired()
            .HasMaxLength(450);
        
        builder.HasIndex(c => c.UserId)
            .IsUnique();
        
        builder.HasMany(c => c.Items)
            .WithOne(i => i.ShoppingCart)
            .HasForeignKey(i => i.ShoppingCartId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Ignore(c => c.DomainEvents);
    }
}