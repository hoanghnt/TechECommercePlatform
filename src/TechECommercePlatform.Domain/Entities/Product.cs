using TechECommercePlatform.Domain.Entities.Common;

namespace TechECommercePlatform.Domain.Entities;

public class Product : BaseAuditableEntity
{
    // Properties
    public string Name { get; private set; } = string.Empty;

    public string Description { get; private set; } = string.Empty;

    public decimal Price { get; private set; }

    public string SKU { get; private set; } = string.Empty; // Stock Keeping Unit

    public string? ImageUrl { get; private set; }

    public bool IsActive { get; private set; }

    // Navigation properties
    public int CategoryId { get; private set; }
    public Category? Category { get; private set; }

    public ICollection<OrderItem> OrderItems { get; private set; } = new List<OrderItem>();
    public ICollection<ShoppingCartItem> ShoppingCartItems { get; private set; } = new List<ShoppingCartItem>();
    public Inventory? Inventory { get; private set; }

    // Constructor for EF Core
    private Product()
    {
    }

    // Factory method - Best practice để tạo entity với validation
    public static Product Create(
        string name,
        string description,
        decimal price,
        string sku,
        int categoryId,
        string? imageUrl = null)
    {
        // Basic validation (có thể move sang Value Object hoặc Domain Service)
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name cannot be empty", nameof(name));

        if (string.IsNullOrWhiteSpace(sku))
            throw new ArgumentException("Product SKU cannot be empty", nameof(sku));
        
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Product description cannot be empty", nameof(description));
        
        if (price <= 0)
            throw new ArgumentException("Price must be greater than zero", nameof(price));

        if (categoryId <= 0)
            throw new ArgumentException("Invalid category ID", nameof(categoryId));
        
        return new Product
        {
            Name = name,
            Description = description,
            Price = price,
            SKU = sku,
            CategoryId = categoryId,
            ImageUrl = imageUrl,
            IsActive = true
        };
    }

    // Business methods
    public void UpdateDetails(string name, string description, decimal price, string? imageUrl)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name cannot be empty", nameof(name));
        
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Product description cannot be empty", nameof(description));

        if (price <= 0)
            throw new ArgumentException("Price must be greater than zero", nameof(price));

        Name = name;
        Description = description;
        Price = price;

        if (imageUrl != null)
            ImageUrl = imageUrl;
    }

    public void Activate() => IsActive = true;

    public void Deactivate() => IsActive = false;

    public void ChangeCategory(int categoryId)
    {
        if (categoryId <= 0)
            throw new ArgumentException("Invalid category ID", nameof(categoryId));

        CategoryId = categoryId;
    }
}