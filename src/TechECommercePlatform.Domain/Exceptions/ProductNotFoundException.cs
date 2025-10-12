namespace TechECommercePlatform.Domain.Exceptions;

public class ProductNotFoundException : NotFoundException
{
    public ProductNotFoundException(string entityName, object key) : base(entityName, key)
    {
    }
}