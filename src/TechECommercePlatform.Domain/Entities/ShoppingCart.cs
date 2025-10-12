using TechECommercePlatform.Domain.Entities.Common;
using TechECommercePlatform.Domain.Exceptions;

namespace TechECommercePlatform.Domain.Entities;

public class ShoppingCart : BaseAuditableEntity
{
    public string UserId { get; private set; } = string.Empty;

    public ICollection<ShoppingCartItem> Items { get; private set; } = new List<ShoppingCartItem>();

    private ShoppingCart()
    {
    }

    public static ShoppingCart Create(
        string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
            throw new ArgumentException("User ID cannot be empty", nameof(userId));
        
        return new ShoppingCart()
        {
            UserId = userId
        };
    }

    public void AddItem(int productId, int quantity)
    {
        if (productId <= 0)
            throw new ArgumentException("Invalid product ID", nameof(productId));
    
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero", nameof(quantity));
        
        // Check if product already in cart
        var existingItem = Items.FirstOrDefault(i => i.ProductId == productId);
    
        if (existingItem != null)
        {
            // Update existing item quantity
            existingItem.UpdateQuantity(existingItem.Quantity + quantity);
        }
        else
        {
            // Add new item
            var newItem = ShoppingCartItem.Create(Id, productId, quantity);
            Items.Add(newItem);
        }
    }

    public void UpdateItemQuantity(int cartItemId, int quantity)
    {
        if (cartItemId <= 0)
            throw new ArgumentException("Invalid cart item ID", nameof(cartItemId));
    
        var item = Items.FirstOrDefault(i => i.Id == cartItemId);
    
        if (item == null)
            throw new NotFoundException(nameof(ShoppingCartItem), cartItemId);
    
        if (quantity <= 0)
        {
            // Remove item if quantity is 0 or negative
            Items.Remove(item);
        }
        else
        {
            item.UpdateQuantity(quantity);
        }
    }

    public void RemoveItem(int cartItemId)
    {
        if (cartItemId <= 0)
            throw new ArgumentException("Invalid cart item ID", nameof(cartItemId));
    
        var item = Items.FirstOrDefault(i => i.Id == cartItemId);
    
        if (item == null)
            throw new NotFoundException(nameof(ShoppingCartItem), cartItemId);
    
        Items.Remove(item);
    }

    public void Clear()
    {
        Items.Clear();
    }

    public decimal GetTotal()
    {
        return Items
            .Where(i => i.Product != null)
            .Sum(i => i.Product!.Price * i.Quantity);
    }
}