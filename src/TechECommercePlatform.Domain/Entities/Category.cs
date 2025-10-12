using TechECommercePlatform.Domain.Entities.Common;

namespace TechECommercePlatform.Domain.Entities;

public class Category : BaseAuditableEntity
{
    //Properties
    public string Name { get; private set; } = string.Empty;

    public string Description { get; private set; } = string.Empty;

    public int? ParentCategoryId { get; private set; }

    // Navigation properties
    public Category? ParentCategory { get; private set; }

    public ICollection<Category> ChildCategories { get; private set; } = new List<Category>();

    public ICollection<Product> Products { get; private set; } = new List<Product>();

    private Category()
    {
    }
    
    // Factory Method
    public static Category Create(
        string name,
        string description,
        int? parentCategoryId = null
    )
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Category name cannot be empty", nameof(name));
        
        return new Category
        {
            Name = name,
            Description = description,
            ParentCategoryId = parentCategoryId
        };
    }
    
    // Business methods
    public void UpdateDetails(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Category name cannot be empty", nameof(name));

        Name = name;
        Description = description;
    }
}