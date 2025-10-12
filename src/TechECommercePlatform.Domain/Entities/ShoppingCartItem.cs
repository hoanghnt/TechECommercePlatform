using TechECommercePlatform.Domain.Entities.Common;

namespace TechECommercePlatform.Domain.Entities;

public class ShoppingCartItem : BaseEntity
{
    public int ShoppingCartId { get; private set; }
    public int ProductId { get; private set; }
    public int Quantity { get; private set; }

    public ShoppingCart? ShoppingCart { get; private set; }
    public Product? Product { get; private set; }

    private ShoppingCartItem()
    {
    }

    public static ShoppingCartItem Create(int shoppingCartId, int productId, int quantity)
    {
        if (shoppingCartId <= 0)
            throw new ArgumentException("Invalid shopping cart ID", nameof(shoppingCartId));
    
        if (productId <= 0)
            throw new ArgumentException("Invalid product ID", nameof(productId));
    
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero", nameof(quantity));
        
        return new ShoppingCartItem
        {
            ShoppingCartId = shoppingCartId,
            ProductId = productId,
            Quantity = quantity
        };
    }

    public void UpdateQuantity(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero", nameof(quantity));
        
        Quantity = quantity;
    }
}