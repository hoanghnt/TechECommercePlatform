using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechECommercePlatform.Domain.Entities;

namespace TechECommercePlatform.Infrastructure.Persistence.Configurations;

public class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
{
    public void Configure(EntityTypeBuilder<Inventory> builder)
    {
        builder.ToTable("Inventories");
        
        builder.HasKey(i => i.Id);
        
        builder.Property(i => i.ProductId)
            .IsRequired();
        
        builder.HasIndex(i => i.ProductId)
            .IsUnique();

        builder.Property(i => i.Quantity)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(i => i.ReservedQuantity)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Ignore(i => i.AvailableQuantity);

        builder.HasOne(i => i.Product)
            .WithOne(i => i.Inventory)
            .HasForeignKey<Inventory>(i => i.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(i => i.DomainEvents);

    }
}